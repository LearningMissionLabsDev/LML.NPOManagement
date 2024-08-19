using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Request;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;


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
                cfg.CreateMap<AccountModel, AccountResponse>();
                cfg.CreateMap<UserModel, UserResponse>();
                cfg.CreateMap<UserIdeaRequest, UserIdeaModel>();
                cfg.CreateMap<UserIdeaModel, UserIdeaResponse>();
                cfg.CreateMap<AddUserToAccountRequest, Account2UserModel>();
                cfg.CreateMap<Account2UserModel, Account2UserResponse>();
                cfg.CreateMap<AccountUserActivityRequest, AccountUserActivityModel>();
                cfg.CreateMap<AccountUserActivityModel,AccountUserActivityResponse>();
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
                    CreatorId = account.CreatorId,
                    StatusId = account.StatusId,
                    MaxCapacity = account.MaxCapacity,
                    IsVisible = account.IsVisible,
                    Name = account.Name,
                    OnboardingLink = account.OnboardingLink,
                    Description = account.Description,                
                    DateCreated = account.DateCreated,
                };
                accountResponses.Add(newAccountResponse);
            }
            return Ok(accountResponses);
        }

        [HttpGet("userProgress/{accountRoleId}")]
        [Authorize((int)UserAccountRoleEnum.SysAdmin | (int)UserAccountRoleEnum.Admin)]
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
        [HttpGet("account")]
        public async Task<ActionResult<AccountResponse>> GetAccountById([FromQuery] int accountId)
        {
            var user = HttpContext.Items["User"] as UserModel;
            if(user == null)
            {
                return Unauthorized();
            }
            var account2User = HttpContext.Items["Account"] as Account2UserModel;

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
                CreatorId = account.CreatorId,
                StatusId = account.StatusId,
                IsVisible = account.IsVisible,
                MaxCapacity = account.MaxCapacity,
                OnboardingLink = account.OnboardingLink,
                Name = account.Name,
                DateCreated = account.DateCreated,
                Description = account.Description,
                AccountImage = account.AccountImage,
                AccountRoleId = account2User?.AccountRoleId               
            };
            HttpContext.Response.Headers.Add("Authorization", user.Token);
            return Ok(accountResponse);
        }
        //???
        [HttpGet("users")]
        public async Task<ActionResult<List<UserInformationResponse>>> GetUsersByAccount()
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
            var usersInfoResponse = new List<UserInformationResponse>();
            foreach (var userInfo in users)
            {
                usersInfoResponse.Add(new UserInformationResponse()
                {
                    UserId = userInfo.Id,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    UserImage = userInfo.UserImage
                });
            }
            return Ok(usersInfoResponse);
        }

        [HttpGet("users/accounts")]
        public async Task<ActionResult<List<AccountResponse>>> GetAccountsByUserId()
        {
            var user = HttpContext.Items["User"] as UserModel;

            if (user == null)
            {
                return StatusCode(401);
            }
            var accounts = await _accountService.GetAccountsByUserId(user.Id);

            if (accounts == null)
            {
                return Conflict();
            }
            var accountResponses = new List<AccountResponse>();
            foreach (var account in accounts)
            {
                var account2User = user.Account2Users.FirstOrDefault(a2u => a2u.AccountId == account.Id);
                var accountRoleId = account2User?.AccountRoleId;

                accountResponses.Add(new AccountResponse()
                {
                    Id = account.Id,
                    Name = account.Name,
                    Description = account.Description,
                    AccountImage = account.AccountImage,
                    AccountRoleId = accountRoleId
                });
            }
            return Ok(accountResponses);
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

        //Done
        [Authorize((int)UserAccountRoleEnum.SysAdmin | (int)UserAccountRoleEnum.Admin
            | (int)UserAccountRoleEnum.AccountManager | (int)UserAccountRoleEnum.Beneficiary)]
        [HttpPost("login")]
        public async Task<ActionResult<Account2UserResponse>> Login([FromQuery] int accountId)
        {
            var user = HttpContext.Items["User"] as UserModel;
            var account2user = HttpContext.Items["Account"] as Account2UserModel;

            if (user == null)
            {
                return Unauthorized("User not logged in!");
            }
            if (account2user == null)
            {
                return StatusCode(403, "Access denied");
            }
            
            var loginToken = await _accountService.AccountLogin(account2user, user);
            if (loginToken == null)
            {
                return StatusCode(403, "Access denied");
            }
            
            HttpContext.Response.Headers.Add("Authorization", loginToken);
            return Ok();
        }

        // DONE
        [HttpPost("addAccount")]
        public async Task<ActionResult<AccountResponse>> AddAccount([FromBody] AccountRequest accountRequest)
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
                MaxCapacity = account.MaxCapacity,
                Name = account.Name,
                Description = account.Description,
                DateCreated = account.DateCreated
            };
            return Ok(accountResponse);
        }

        [HttpPost("addUserActivity")]
        public async Task<ActionResult<AccountUserActivityResponse>> AddAccountUserActivityProgress([FromBody] AccountUserActivityRequest accountUserActivityRequest)
        {
            var account = HttpContext.Items["Account"] as Account2UserModel;
            if (account == null)
            {
                return StatusCode(403, "Access denied");
            }
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
                IsVisible = accountRequest.IsVisible,
                MaxCapacity = accountRequest.MaxCapacity,
                OnboardingLink = accountRequest.OnboardingLink,
                Description = accountRequest.Description,
                StatusId = accountRequest.StatusId
            };
            var modifyAccount = await _accountService.ModifyAccount(accountModel);

            if (modifyAccount == null)
            {
                return BadRequest();
            }
            var accountResponse = new AccountResponse()
            {
                Id = modifyAccount.Id,
                StatusId = modifyAccount.StatusId,
                CreatorId = modifyAccount.CreatorId,
                IsVisible = modifyAccount.IsVisible,
                MaxCapacity = modifyAccount.MaxCapacity,
                Name = modifyAccount.Name,
                OnboardingLink = modifyAccount.OnboardingLink,
                Description = modifyAccount.Description
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
