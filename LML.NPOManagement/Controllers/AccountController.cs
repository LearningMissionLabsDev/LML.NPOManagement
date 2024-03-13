using AutoMapper;
using DotNetOpenAuth.InfoCard;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Request;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = LML.NPOManagement.Bll.Services.AuthorizeAttribute;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMapper _mapper;
        private IConfiguration _configuration;
        private IAccountService _accountService;
        private INotificationService _notificationService;
        private IUserService _userService;
        public AccountController(IConfiguration configuration, IAccountService accountService, INotificationService notificationService, IUserService userService)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountRequest, AccountModel>();
                cfg.CreateMap<AccountProgressRequest, AccountProgressModel>();
                cfg.CreateMap<AccountModel, AccountResponse>();
                cfg.CreateMap<AccountProgressModel, AccountProgressResponse>();
                cfg.CreateMap<UserModel, UserResponse>();
                cfg.CreateMap<UserIdeaRequest, UserIdeaModel>();
                cfg.CreateMap<UserIdeaModel, UserIdeaResponse>();
            });
            _mapper = config.CreateMapper();
            _configuration = configuration;
            _accountService = accountService;
            _notificationService = notificationService;
            _userService = userService;
        }

        // DONE
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<AccountResponse>>> GetAccounts()
        {
            var accounts = await _accountService.GetAllAccounts();
            if (accounts == null)
            {
                return NotFound();
            }
            var accountResponses = new List<AccountResponse>();

            foreach (var account in accounts)
            {
                var newAccountResponse = new AccountResponse()
                {
                    Id = account.Id,
                    Name = account.Name,
                    Description = account.Description,
                    StatusId = account.StatusId,
                };
                accountResponses.Add(newAccountResponse);
            }

            return Ok(accountResponses);
        }

        [HttpGet("userProgress/{accountRoleId}")]
        [Authorize("Admin", "AccountManager")]
        public async Task<ActionResult<List<AccountUserActivityResponse>>> GetAccountRoleProgress(int accountRoleId)
        {
            var account = HttpContext.Items["Account"] as AccountModel;
            if (account == null)
            {
                return Unauthorized();
            }
            var userActivities = await _accountService.GetAccountRoleProgress(account.Id, accountRoleId);
            if (userActivities == null)
            {
                return NotFound();
            }
            var accountUserResponses = new List<AccountUserActivityResponse>();

            foreach (var userProgress in userActivities)
            {
                var newAccountUserResponse = new AccountUserActivityResponse()
                {
                    Id = userProgress.Id,
                    Account2UserId = userProgress.Account2UserId,
                    ActivityInfo = userProgress.ActivityInfo,
                    DateCreated = userProgress.DateCreated,
                };
                accountUserResponses.Add(newAccountUserResponse);
            }
            return Ok(accountUserResponses);
        }

        // DONE
        [HttpGet("{accountId}")]
        public async Task<ActionResult<AccountResponse>> GetAccountById(int accountId)
        {
            if (accountId <= 0)
            {
                return BadRequest();
            }
            var account = await _accountService.GetAccountById(accountId);

            if (account == null)
            {
                return NotFound();
            }
            var accountResponse = new AccountResponse()
            {
                Id = account.Id,
                Name = account.Name,
                Description = account.Description,
                StatusId = account.StatusId,
            };
            return Ok(accountResponse);
        }

        [HttpGet("users/{accountId}")]
        public async Task<ActionResult<List<UserResponse>>> GetUsersByAccount(int accountId)
        {
            if (accountId <= 0)
            {
                return BadRequest();
            }
            var users = await _accountService.GetUsersByAccount(accountId);

            if (users == null)
            {
                return NotFound();
            }
            var userResponse = new List<UserResponse>();
            foreach (var user in users)
            {
                userResponse.Add(new UserResponse()
                {
                    Id = user.Id,
                    Email = user.Email
                });
            }
            return Ok(userResponse);
        }

        // DONE
        [HttpGet("search/{accountName}")]
        public async Task<ActionResult<List<AccountResponse>>> GetAccountsByName(string accountName)
        {
            if (string.IsNullOrEmpty(accountName))
            {
                return BadRequest();
            }
            var accountModel = await _accountService.GetAccountsByName(accountName);

            if (accountModel == null)
            {
                return NotFound();
            }
            var accounts = new List<AccountResponse>();

            foreach (var account in accountModel)
            {
                var accountResponse = new AccountResponse()
                {
                    Id = account.Id,
                    Name = account.Name,
                    Description = account.Description,
                    StatusId = account.StatusId
                };
                accounts.Add(accountResponse);
            }
            return Ok(accounts);
        }

        [HttpPost("login/{accountId}")]
        public async Task<ActionResult<Account2UserModel>> Login(int accountId)
        {
            var user = HttpContext.Items["User"] as UserModel;

            if (user == null)
            {
                return Unauthorized("User not logged in!");
            }
            var account = user.Account2Users.FirstOrDefault(acc => acc.AccountId == accountId);  
            if(account == null)
            {
                return StatusCode(500,"Account Not Found");
            }
            var login = await _accountService.AccountLogin(account.AccountId, _configuration);

            if (login == null)
            {
                return Conflict();
            }
            return Ok(login);
        }

        // DONE
        [HttpPost("addAccount")]
        public async Task<ActionResult<AccountResponse>> AddACcount([FromBody] AccountRequest accountRequest)
        {
            var user = HttpContext.Items["User"] as UserModel;
            if (user == null)
            {
                return Unauthorized();
            }
            var accountModel = _mapper.Map<AccountRequest, AccountModel>(accountRequest);
            accountModel.CreatorId = user.Id;
            var account = await _accountService.AddAccount(accountModel);

            if (account == null)
            {
                return BadRequest("Process Failed!");
            }
            var accountResponse = new AccountResponse()
            {
                Id = account.Id,
                StatusId = account.StatusId,
                CreatorId = account.CreatorId,
                Name = account.Name,
                Description = account.Description,
                DateCreated = account.DateCreated
            };
            return Ok(accountResponse);
        }

        // DONE
        [HttpPost("addUser")]
        public async Task<ActionResult> AddUserToAccount([FromBody] AddUserToAccountRequest addUserToAccountRequest)
        {
            if ((addUserToAccountRequest.AccountId <= 0 || addUserToAccountRequest.UserId <= 0) || (int)addUserToAccountRequest.UserAccountRoleEnum < 1 || (int)addUserToAccountRequest.UserAccountRoleEnum > 3)
            {
                return BadRequest();
            }
            var account = await _accountService.AddUserToAccount(addUserToAccountRequest.AccountId, addUserToAccountRequest.UserId, (int)addUserToAccountRequest.UserAccountRoleEnum);

            if (!account)
            {
                return Conflict();
            }
            return Ok();
        }

        // DONE
        [HttpPut("{accountId}")]
        public async Task<ActionResult> ModifyAccount(int accountId, [FromBody] AccountRequest accountRequest)
        {
            if (accountId <= 0)
            {
                throw new ArgumentException("Account Not Valid!");
            }
            var accountModel = _mapper.Map<AccountRequest, AccountModel>(accountRequest);
            var modifyAccount = await _accountService.ModifyAccount(accountModel, accountId);

            if (modifyAccount == null)
            {
                return BadRequest();
            }
            return Ok(modifyAccount);
        }

        // DONE
        [HttpDelete("removeUser")]
        public async Task<ActionResult> RemoveUserFromAccount(int accountId, int userId)
        {
            if (accountId <= 0 || userId <= 0)
            {
                return BadRequest("Unable to remove user from account");
            }
            var user = await _accountService.RemoveUserFromAccount(accountId, userId);

            if (!user)
            {
                return NotFound();
            }
            return Ok();
        }

        // DONE
        [HttpDelete("{accountId}")]
        public async Task<ActionResult> DeleteAccount(int accountId)
        {
            if (accountId <= 0)
            {
                return BadRequest();
            }
            var account = await _accountService.DeleteAccount(accountId);

            if (!account)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
