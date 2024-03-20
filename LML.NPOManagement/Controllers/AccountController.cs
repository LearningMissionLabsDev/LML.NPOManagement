using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Request;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;


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
                cfg.CreateMap<AddUserToAccountRequest, Account2UserModel>();
                cfg.CreateMap<Account2UserModel, Account2UserResponse>();
            });
            _mapper = config.CreateMapper();
            _configuration = configuration;
            _accountService = accountService;
            _notificationService = notificationService;
            _userService = userService;
        }

        // DONE
        [HttpGet]
        //[Authorize]
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
                    StatusId = account.StatusId
                };
                accountResponses.Add(newAccountResponse);
            }
            return Ok(accountResponses);
        }

        [HttpGet("userProgress/{accountRoleId}")]
        //[Authorize("Admin", "AccountManager")]
        public async Task<ActionResult<List<AccountUserActivityResponse>>> GetAccountRoleProgress(int accountRoleId)
        {
            if (accountRoleId < 1 || accountRoleId > 3)
            {
                return BadRequest();
            }
            var account = HttpContext.Items["Account"] as Account2UserModel;
            if (account == null)
            {
                return Unauthorized();
            }
            var userActivities = await _accountService.GetAccountRoleProgress(account.AccountId, accountRoleId);

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
                DateCreated = account.DateCreated,
            };
            return Ok(accountResponse);
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<UserResponse>>> GetUsersByAccount()
        {
            var account = HttpContext.Items["Account"] as Account2UserModel;
            if (account == null)
            {
                return BadRequest();
            }
            var users = await _accountService.GetUsersByAccount(account.AccountId);

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
                    StatusId = account.StatusId,
                    CreatorId = account.CreatorId,
                    DateCreated = account.DateCreated
                };
                accounts.Add(accountResponse);
            }
            return Ok(accounts);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Account2UserResponse>> Login()
        {
            var user = HttpContext.Items["User"] as UserModel;
            var account = HttpContext.Items["Account"] as Account2UserModel;

            if (user == null)
            {
                return Unauthorized("User not logged in!");
            }
            if (account == null)
            {
                return StatusCode(403, "Access denied");
            }

            var account2user = new Account2UserModel()
            {
                AccountId = account.AccountId,
                UserId = account.UserId,
                AccountRoleId = account.AccountRoleId
            };

            var login = await _accountService.AccountLogin(account2user);

            if (login == null)
            {
                return Conflict();
            }
            var accountResponse = new Account2UserResponse()
            {
                AccountId = login.AccountId,
                UserId = login.UserId,
                AccountRoleId = login.AccountRoleId
            };
            return Ok(accountResponse);
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

        [HttpPost("addUserActivity")]
        public async Task<ActionResult<AccountUserActivityResponse>> AddAccountUserActivityProgress([FromBody] AccountUserActivityRequest accountUserActivityRequest)
        {
            var activityModel = _mapper.Map<AccountUserActivityModel>(accountUserActivityRequest);
            var activityUser = await _accountService.AddAccountUserActivityProgress(activityModel);
            if (activityUser == null)
            {
                return Conflict();
            }
            var activityResponse = new AccountUserActivityResponse()
            {
                Id = activityUser.Id,
                Account2UserId = activityUser.Account2UserId,
                ActivityInfo = activityUser.ActivityInfo,
                DateCreated = activityUser.DateCreated
            };
            return Ok(activityResponse);
        }

        // DONE
        [HttpPost("addUser")]
        public async Task<ActionResult> AddUserToAccount([FromBody] AddUserToAccountRequest addUserToAccountRequest)
        {
            var account = HttpContext.Items["Account"] as Account2UserModel;
            if (account == null)
            {
                return NotFound("Access denied");
            }
            var account2User = new Account2UserModel()
            {
                AccountId = account.AccountId,
                UserId = addUserToAccountRequest.UserId,
                AccountRoleId = (int)addUserToAccountRequest.UserAccountRoleEnum
            };
            var result = await _accountService.AddUserToAccount(account2User);

            if (!result)
            {
                return Conflict();
            }
            return Ok();
        }

        // DONE
        [HttpPut("modifyAccount")]
        public async Task<ActionResult<AccountResponse>> ModifyAccount([FromBody] AccountRequest accountRequest)
        {
            var account = HttpContext.Items["Account"] as Account2UserModel;
            if (account == null)
            {
                return StatusCode(403, "Access denied");
            }
            var accountModel = new AccountModel()
            {
                Id = account.AccountId,
                Name = accountRequest.Name,
                Description = accountRequest.Description,
                StatusId = (int)accountRequest.StatusEnum
            };
            var modifyAccount = await _accountService.ModifyAccount(accountModel);

            if (modifyAccount == null)
            {
                return BadRequest();
            }
            var accountResponse = new AccountResponse()
            {
                Id = modifyAccount.Id,
                CreatorId = modifyAccount.CreatorId,
                Name = modifyAccount.Name,
                Description = modifyAccount.Description,
                StatusId = modifyAccount.StatusId,
            };
            return Ok(accountResponse);
        }

        // DONE
        [HttpDelete("removeUser/{userId}")]
        public async Task<ActionResult> RemoveUserFromAccount(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("User Not Found");
            }
            var account = HttpContext.Items["Account"] as Account2UserModel;
            if (account == null)
            {
                return StatusCode(403, "Access denied");
            }
            var user = await _accountService.RemoveUserFromAccount(account.AccountId, userId);

            if (!user)
            {
                return Conflict();
            }
            return Ok();
        }

        // DONE
        [HttpDelete("deleteAccount")]
        public async Task<ActionResult> DeleteAccount()
        {
            var account = HttpContext.Items["Account"] as Account2UserModel;
            if (account == null)
            {
                return NotFound("Account Not Found");
            }
            var deletedAccount = await _accountService.DeleteAccount(account.AccountId);

            if (!deletedAccount)
            {
                return Conflict();
            }
            return Ok();
        }
    }
}
