using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Request;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
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
                cfg.CreateMap<AccountUserActivityModel, AccountUserActivityResponse>();
            });
            _mapper = config.CreateMapper();
            _accountService = accountService;
        }

        [HttpGet]
        [Authorize(RoleAccess.AccountAdmin)]
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
                    AccountImage = account.AccountImage,
                    DeletedAt = account.DeletedAt
                };
                accountResponses.Add(newAccountResponse);
            }

            return Ok(accountResponses);
        }

        [HttpGet("exclude-admins")]
        public async Task<ActionResult<List<AccountResponse>>> GetAllAccountsExceptAdmins()
        {
            var accounts = await _accountService.GetAllAccountsExceptAdmins();
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
                    AccountImage = account.AccountImage,
                    DeletedAt = account.DeletedAt
                };
                accountResponses.Add(newAccountResponse);
            }

            return Ok(accountResponses);
        }

        [HttpGet("filter")]
        [Authorize(RoleAccess.SysAdminOnly)]
        public async Task<ActionResult<List<AccountResponse>>> GetAccountsByStatus([FromQuery] List<int>? statusIds)
        {
            var accounts = await _accountService.GetAccountsByStatus(statusIds);
            if (accounts == null)
            {
                return NotFound("Accounts by this criteria not found.");
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
                    AccountImage = account.AccountImage,
                    DeletedAt = account.DeletedAt
                };
                accountResponses.Add(newAccountResponse);
            }

            return Ok(accountResponses);
        }

        [HttpGet("userProgress/{accountRoleId}")]
        [Authorize(RoleAccess.AccountAdmin)]
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

        [HttpGet("{accountId}")]
        [Authorize(RoleAccess.AllAccess)]
        public async Task<ActionResult<AccountResponse>> GetAccountById([FromQuery] int accountId)
        {
            var user = HttpContext.Items["User"] as UserModel;
            if (user == null)
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

        [HttpGet("info/{accountId}")]
        [Authorize(RoleAccess.AllAccess)]
        public async Task<ActionResult<AccountResponse>> GetInfoAccountById(int accountId)
        {
            if (HttpContext.Items["User"] is not UserModel)
            {
                return Unauthorized();
            }

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
                StatusId = account.StatusId,
                IsVisible = account.IsVisible,
                MaxCapacity = account.MaxCapacity,
                OnboardingLink = account.OnboardingLink,
                Name = account.Name,
                DateCreated = account.DateCreated,
                Description = account.Description,
                AccountImage = account.AccountImage
            };

            return Ok(accountResponse);
        }

        [HttpGet("users")]
        [Authorize(RoleAccess.AllAccess)]
        public async Task<ActionResult<List<UserInformationResponse>>> GetUsersByAccount([FromQuery] int accountId)
        {
            var account = await _accountService.GetAccountById(accountId);
            if (account == null)
            {
                return BadRequest();
            }

            var users = await _accountService.GetUsersByAccount(accountId);
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

        [HttpGet("search/{accountName}")]
        [Authorize(RoleAccess.SysAdminOnly)]
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

        [Authorize(RoleAccess.AllAccess)]
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

        [HttpPost("addAccount")]
        [Authorize(RoleAccess.SysAdminOnly)]
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
                DateCreated = account.DateCreated,
                AccountImage= accountRequest.AccountImage
            };

            return Ok(accountResponse);
        }

        [HttpPost("addUserActivity")]
        [Authorize(RoleAccess.AdminsAndManager)]
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

        [HttpPost("addUser")]
        [Authorize(RoleAccess.AdminsAndManager)]
        public async Task<ActionResult> AddUserToAccount([FromBody] AddUserToAccountRequest addUserToAccountRequest)
        {
            var account = await _accountService.GetAccountById(addUserToAccountRequest.AccountId);
            if (account == null && addUserToAccountRequest.UserId < 0)
            {
                return BadRequest();
            }

            var account2User = new Account2UserModel()
            {
                AccountId = addUserToAccountRequest.AccountId,
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

        [HttpPut("modifyAccount/{accountId}")]
        [Authorize(RoleAccess.AccountAdmin)]
        public async Task<ActionResult<AccountResponse>> ModifyAccount([FromBody] AccountRequest accountRequest, int accountId)
        {
            var account = await _accountService.GetAccountById(accountId);
            if (account == null || accountId < 0)
            {
                return BadRequest();
            }

            var accountModel = new AccountModel()
            {
                Id = accountId,
                Name = accountRequest.Name,
                IsVisible = accountRequest.IsVisible,
                MaxCapacity = accountRequest.MaxCapacity,
                OnboardingLink = accountRequest.OnboardingLink,
                Description = accountRequest.Description,
                StatusId = accountRequest.StatusId,
                AccountImage = accountRequest.AccountImage
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
                Description = modifyAccount.Description,
                AccountImage = modifyAccount.AccountImage
            };

            return Ok(accountResponse);
        }

        [HttpDelete("removeUser/{userId}")]
        [Authorize(RoleAccess.AdminsAndManager)]
        public async Task<ActionResult> RemoveUserFromAccount(int userId, int accountId)
        {
            if (userId <= 0 || accountId < 0)
            {
                return BadRequest();
            }

            var account = await _accountService.GetAccountById(accountId);
            if (account == null)
            {
                return BadRequest();
            }

            var user = await _accountService.RemoveUserFromAccount(accountId, userId);
            if (!user)
            {
                return Conflict();
            }

            return Ok();
        }


        [HttpDelete("{accountId}")]
        [Authorize(RoleAccess.AccountAdmin)]
        public async Task<ActionResult> DeleteAccountById(int accountId)
        {
            if (accountId <= 0)
            {
                return NotFound("Account Not Found");
            }

            var deletedAccount = await _accountService.DeleteAccount(accountId);
            if (!deletedAccount)
            {
                return Conflict();
            }

            return Ok();
        }
    }
}