using Amazon.S3;
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
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly IConfiguration _configuration;
        private readonly IAmazonS3 _s3Client;

        public UserController(IUserService userService, INotificationService notificationService, IConfiguration configuration, IAmazonS3 s3Client)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRequest, UserModel>();
                cfg.CreateMap<LoginRequest, UserModel>();
                cfg.CreateMap<UserModel, UserResponse>();
                cfg.CreateMap<SearchModel, SearchResponse>();
                cfg.CreateMap<UserInformationRequest, UserInformationModel>();
                cfg.CreateMap<UserInformationModel, UserInformationResponse>();
                cfg.CreateMap<UsersGroupRequest, UsersGroupModel>();
                cfg.CreateMap<UsersGroupModel, UsersGroupResponse>();
                cfg.CreateMap<Account2UserRequest, Account2UserModel>();
            });
            _mapper = config.CreateMapper();
            _userService = userService;
            _notificationService = notificationService;
            _configuration = configuration;
            _s3Client = s3Client;
        }

        [HttpGet]
        [Authorize(RoleAccess.AllAccess)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            if (users == null)
            {
                return NotFound();
            }

            var userCredentialResponse = new List<UserCredentialResponse>();
            foreach (var user in users)
            {
                var userInfo = user.UserInformations.FirstOrDefault();
                if (userInfo != null)
                {
                    userCredentialResponse.Add(new UserCredentialResponse()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        StatusId = user.StatusId,
                        FirstName = userInfo.FirstName,
                        LastName = userInfo.LastName,
                        PhoneNumber = userInfo.PhoneNumber,
                        DateOfBirth = userInfo.DateOfBirth,
                        MiddleName = userInfo.MiddleName,
                        Gender = userInfo.Gender,
                        RequestedUserRoleId = userInfo.RequestedUserRoleId,
                        CreateDate = userInfo.CreateDate,
                        UpdateDate = userInfo.UpdateDate,
                        DeletedAt = userInfo.DeletedAt
                    });
                }
            }

            return Ok(userCredentialResponse);
        }

        [HttpGet("filter")]
        [Authorize(RoleAccess.SysAdminOnly)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsersFiltered(
           [FromQuery] int? statusId,
           [FromQuery] string? firstName,
           [FromQuery] string? lastName)
        {
            var users = await _userService.GetUsersByCriteria(statusId, firstName, lastName);
            if (users == null || !users.Any())
            {
                return NotFound("Users By This Criteria Not Found");
            }

            var userCredentialResponse = new List<UserCredentialResponse>();
            foreach (var user in users)
            {
                var userInfo = user.UserInformations.FirstOrDefault();
                if (userInfo != null)
                {
                    userCredentialResponse.Add(new UserCredentialResponse()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        StatusId = user.StatusId,
                        FirstName = userInfo.FirstName,
                        LastName = userInfo.LastName,
                        PhoneNumber = userInfo.PhoneNumber,
                        DateOfBirth = userInfo.DateOfBirth,
                        MiddleName = userInfo.MiddleName,
                        Gender = userInfo.Gender,
                        RequestedUserRoleId = userInfo.RequestedUserRoleId,
                        CreateDate = userInfo.CreateDate,
                        UpdateDate = userInfo.UpdateDate,
                        UserImage = userInfo.UserImage,
                        DeletedAt = userInfo.DeletedAt
                    });
                }
            }

            return Ok(userCredentialResponse);
        }

        [HttpGet("groups")]
        [Authorize(RoleAccess.AllAccess)]
        public async Task<ActionResult<IEnumerable<UsersGroupResponse>>> GetGroups()
        {
            var groupsModel = await _userService.GetAllGroups();
            if (groupsModel == null)
            {
                return NotFound();
            }

            var groups = new List<UsersGroupResponse>();
            foreach (var group in groupsModel)
            {
                var groupResponse = new UsersGroupResponse()
                {
                    Id = group.Id,
                    CreatorId = group.CreatorId,
                    GroupName = group.GroupName,
                    Description = group.Description,
                };
                groups.Add(groupResponse);
            }

            return Ok(groups);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserCredentialResponse>> GetUserbyId(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest();
            }

            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }

            var userInfo = user.UserInformations.First();
            var userCredentialResponse = new UserCredentialResponse()
            {
                Id = user.Id,
                Email = user.Email,
                StatusId = user.StatusId,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                PhoneNumber = userInfo.PhoneNumber,
                MiddleName = userInfo.MiddleName,
                Gender = userInfo.Gender,
                RequestedUserRoleId = userInfo.RequestedUserRoleId,
                DateOfBirth = userInfo.DateOfBirth,
                CreateDate = userInfo.CreateDate,
                UpdateDate = userInfo.UpdateDate,
                Metadata = userInfo.Metadata,
                UserImage = userInfo.UserImage
            };

            return Ok(userCredentialResponse);
        }

        [HttpGet("group/{groupId}")]
        [Authorize(RoleAccess.AllAccess)]
        public async Task<ActionResult<UsersGroupResponse>> GetGroupById(int groupId)
        {
            if (groupId <= 0)
            {
                return BadRequest();
            }
            var groupModel = await _userService.GetGroupById(groupId);

            if (groupModel == null)
            {
                return NotFound();
            }

            var groupResponse = new UsersGroupResponse()
            {
                Id = groupModel.Id,
                CreatorId = groupModel.CreatorId,
                GroupName = groupModel.GroupName,
                Description = groupModel.Description,
            };

            return Ok(groupResponse);
        }

        [HttpGet("group/search/{groupName}")]
        [Authorize(RoleAccess.AllAccess)]
        public async Task<ActionResult<List<UsersGroupResponse>>> GetGroupsByName(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return BadRequest();
            }

            var groupModel = await _userService.GetGroupsByName(groupName);
            if (groupModel == null)
            {
                return NotFound();
            }

            var groups = new List<UsersGroupResponse>();
            foreach (var group in groupModel)
            {
                var groupResponse = new UsersGroupResponse()
                {
                    Id = group.Id,
                    CreatorId = group.CreatorId,
                    GroupName = group.GroupName,
                    Description = group.Description,
                };
                groups.Add(groupResponse);
            }

            return Ok(groups);
        }

        [HttpGet("group/user/{userId}")]
        [Authorize(RoleAccess.AllAccess)]
        public async Task<ActionResult<List<UsersGroupResponse>>> GetGroupsForUser(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest();
            }

            var usersGroupModel = await _userService.GetGroupsForUser(userId);
            if (usersGroupModel == null)
            {
                return NotFound();
            }

            var userGroups = new List<UsersGroupResponse>();
            foreach (var user in usersGroupModel)
            {
                var userResponse = new UsersGroupResponse()
                {
                    Id = user.Id,
                    CreatorId = user.CreatorId,
                    GroupName = user.GroupName,
                    Description = user.Description
                };
                userGroups.Add(userResponse);
            }

            return Ok(userGroups);
        }

        [HttpGet("group/members/{groupId}")]
        [Authorize(RoleAccess.AllAccess)]
        public async Task<ActionResult<List<UserResponse>>> GetUsersByGroupId(int groupId)
        {
            if (groupId <= 0)
            {
                return BadRequest();
            }

            var usersModel = await _userService.GetUsersByGroupId(groupId);
            if (usersModel == null || !usersModel.Any())
            {
                return NotFound();
            }

            var users = new List<UserResponse>();
            foreach (var user in usersModel)
            {
                var usersResponse = new UserResponse()
                {
                    Id = user.Id,
                    Email = user.Email
                };
                users.Add(usersResponse);
            }

            return Ok(users);
        }

        [HttpGet("idea")]
        [Authorize(RoleAccess.AllAccess)]
        public async Task<ActionResult<List<UserIdeaResponse>>> GetIdeas()
        {
            var ideas = await _userService.GetAllIdeas();
            if (ideas == null)
            {
                return NotFound();
            }

            var ideaResponses = new List<UserIdeaResponse>();
            foreach (var idea in ideas)
            {
                var ideaResponse = new UserIdeaResponse()
                {
                    Id = idea.Id,
                    UserId = idea.UserId,
                    IdeaDescription = idea.IdeaDescription,
                    IdeaCategory = idea.IdeaCategory,
                };
                ideaResponses.Add(ideaResponse);
            }

            return Ok(ideaResponses);
        }

        [HttpPost("submitComments")]
        [Authorize(RoleAccess.AllAccess)]
        public async Task<ActionResult> SubmitComments([FromBody] UserIdeaRequest userIdeaRequest)
        {
            var user = HttpContext.Items["User"] as UserModel;
            if (user == null)
            {
                return BadRequest();
            }

            var ideaModel = _mapper.Map<UserIdeaRequest, UserIdeaModel>(userIdeaRequest);
            ideaModel.UserId = user.Id;

            var idea = await _userService.AddUserIdea(ideaModel);
            if (idea == null)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpGet("verifyEmail")]
        public async Task<ActionResult> VerifyEmail([FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Please check your token");
            }

            var user = await _userService.ActivationUser(token, _configuration);
            var bucketName = _configuration.GetSection("AppSettings:BucketName").Value;
            var template = _configuration.GetSection("AppSettings:Templates").Value;
            var key = template + "RegistracionNotification.html";
            var body = await GetFileByKeyAsync(bucketName, key);

            _notificationService.SendNotificationUserAsync(user, new NotificationModel(), body);

            return Ok();
        }

        [HttpGet("logout")]
        [Authorize(RoleAccess.AllAccess)]
        public ActionResult LogOut()
        {
            var user = HttpContext.Items["User"] as UserModel;
            if (user != null)
            {
                user.Token = null;
                return Ok();
            }

            return BadRequest("Not User");
        }

        [HttpGet("search/{searchParam}/{includeGroups}")]
        [Authorize(RoleAccess.AllAccess)]
        public async Task<ActionResult<List<SearchResponse>>> SearchByName(string searchParam, bool includeGroups)
        {
            if (string.IsNullOrEmpty(searchParam))
            {
                return BadRequest("This field is required!");
            }

            var searchResults = await _userService.GetSearchResults(searchParam, includeGroups);
            if (searchResults == null || !searchResults.Any())
            {
                return NotFound("Users Not Found");
            }

            var searchResponses = _mapper.Map<List<SearchModel>, List<SearchResponse>>(searchResults);
            return Ok(searchResponses);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            var userModel = _mapper.Map<LoginRequest, UserModel>(loginRequest);
            var user = await _userService.Login(userModel, _configuration);

            if (user != null)
            {
                var accounts = user.Account2Users.ToList();
                var userResponse = new UserResponse()
                {
                    Id = user.Id,
                    Email = userModel.Email,
                    UserAccounts = accounts.Select(x => new AccountMappingResponse() { AccountId = x.AccountId, AccountName = x.Account?.Name, AccountRoleId = x.AccountRoleId }).ToList()
                };

                if (user.StatusId == (int)StatusEnumModel.Active)
                {
                    HttpContext.Response.Headers.Add("Authorization", user.Token);
                    return Ok(userResponse);
                }
                return Conflict();
            }
            return Unauthorized(401);
        }

        [HttpPost("registration")]
        public async Task<ActionResult<UserModel>> Registration([FromBody] UserRequest userRequest)
        {
            if (userRequest.Password != userRequest.ConfirmPassword)
            {
                return StatusCode(409);
            }

            var userModel = _mapper.Map<UserRequest, UserModel>(userRequest);
            var result = await _userService.Registration(userModel, _configuration);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("userInfoRegistration")]
        public async Task<ActionResult<int>> UserInfoRegistration([FromBody] UserInformationRequest userInformationRequest)
        {
            var user = HttpContext.Items["User"] as UserModel;
            if (user == null)
            {
                return BadRequest();
            }

            var userInformationModel = new UserInformationModel()
            {
                RequestedUserRoleId = (int)userInformationRequest.UserTypeEnum,
                UserId = user.Id,
                Gender = userInformationRequest.Gender,
                FirstName = userInformationRequest.FirstName,
                LastName = userInformationRequest.LastName,
                MiddleName = userInformationRequest.MiddleName,
                Metadata = userInformationRequest.UserMetadata,
                PhoneNumber = userInformationRequest.PhoneNumber,
                DateOfBirth = userInformationRequest.DateOfBirth,
            };

            var userInfoId = await _userService.UserInformationRegistration(userInformationModel, _configuration);
            return Ok(userInfoId);
        }

        [HttpPost("group")]
        [Authorize(RoleAccess.AllAccess)]
        public async Task<ActionResult<UsersGroupResponse>> AddGroup([FromBody] UsersGroupRequest usersGroupRequest)
        {
            var user = HttpContext.Items["User"] as UserModel;
            if (user == null)
            {
                return BadRequest("Please logged in or check account verification!");
            }

            var usersGroupModel = _mapper.Map<UsersGroupRequest, UsersGroupModel>(usersGroupRequest);
            usersGroupModel.CreatorId = user.Id;

            var newUsersGroupModel = await _userService.CreateGroup(usersGroupModel);
            if (newUsersGroupModel == null)
            {
                return BadRequest("Your Request Is Not Valid");
            }

            var newUsersGroupResponse = _mapper.Map<UsersGroupModel, UsersGroupResponse>(newUsersGroupModel);
            return Ok(newUsersGroupResponse);
        }

        [HttpPost("group/addUser")]
        [Authorize(RoleAccess.AllAccess)]
        public async Task<ActionResult> AddUserToGroup([FromBody] AddUserToGroupRequest addUserToGroupRequest)
        {
            if (addUserToGroupRequest.UserId <= 0 || addUserToGroupRequest.GroupId <= 0)
            {
                return BadRequest("Process Failed");
            }

            var result = await _userService.AddUserToGroup(addUserToGroupRequest.UserId, addUserToGroupRequest.GroupId);
            if (!result)
            {
                return Conflict();
            }

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserRequest userRequest)
        {
            var user = HttpContext.Items["User"] as UserModel;
            if (user == null)
            {
                return Unauthorized();
            }

            var userModel = _mapper.Map<UserRequest, UserModel>(userRequest);
            var modifyUser = await _userService.ModifyUserCredentials(userModel.Email, userModel.Password, user.Id);
            if (modifyUser != null)
            {
                if (modifyUser.StatusId == (int)StatusEnumModel.Pending)
                {
                    var bucketName = _configuration.GetSection("AppSettings:BucketName").Value;
                    var key = "NotificationTemplates/CheckingEmail.html";
                    var body = await GetFileByKeyAsync(bucketName, key);

                    _notificationService.CheckingEmail(modifyUser, new NotificationModel(), _configuration, body);
                }
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("userInfo")]
        public async Task<ActionResult> PutUserInfo([FromBody] UserCredentialRequest userCredentialRequest)
        {
            var user = HttpContext.Items["User"] as UserModel;
            if (user == null)
            {
                return BadRequest("Please logged in");
            }

            var userInfoModel = new UserCredential()
            {
                Email = userCredentialRequest.Email,
                StatusId = userCredentialRequest.StatusId,
                UserId = userCredentialRequest.Id,
                RequestedUserRoleId = userCredentialRequest.RequestedUserRoleId,
                Gender = userCredentialRequest.Gender,
                FirstName = userCredentialRequest.FirstName,
                LastName = userCredentialRequest.LastName,
                MiddleName = userCredentialRequest.MiddleName,
                Metadata = userCredentialRequest.Metadata,
                PhoneNumber = userCredentialRequest.PhoneNumber,
                DateOfBirth = userCredentialRequest.DateOfBirth,
                UserImage = userCredentialRequest.UserImage
            };

            var modifyUser = await _userService.ModifyUserInfo(userInfoModel);
            if (modifyUser)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{userId}")]
        [Authorize(RoleAccess.SysAdminOnly)]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest();
            }

            var deletedUser = await _userService.DeleteUser(userId);
            if (deletedUser == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("group/{groupId}/{userId}")]
        [Authorize(RoleAccess.AdminsAndManager)]
        public async Task<ActionResult> DeleteUserFromGroup(int userId, int groupId)
        {
            if (userId <= 0 || groupId <= 0)
            {
                return BadRequest("Unable to remove user from group");
            }

            var user = await _userService.DeleteUserFromGroup(userId, groupId);
            if (!user)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("group")]
        [Authorize(RoleAccess.AdminsAndManager)]
        public async Task<ActionResult> DeleteGroup(int groupId)
        {
            if (groupId <= 0)
            {
                return BadRequest();
            }

            var group = await _userService.DeleteGroup(groupId);
            if (!group)
            {
                return NotFound();
            }

            return Ok();
        }

        private async Task<string> GetFileByKeyAsync(string bucketName, string key)
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists)
            {
                return null;
            }

            var s3Object = await _s3Client.GetObjectAsync(bucketName, key);
            var streamReader = new StreamReader(s3Object.ResponseStream).ReadToEnd();

            return streamReader;
        }
    }
}