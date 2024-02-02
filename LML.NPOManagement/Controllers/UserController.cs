using Amazon.S3;
using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Common;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Request;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;
using LML.NPOManagement.Common.Model;
using System.Text.RegularExpressions;

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        private IUserService _userService;
        private INotificationService _notificationService;
        private IConfiguration _configuration;
        private IAmazonS3 _s3Client;

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

            });
            _mapper = config.CreateMapper();
            _userService = userService;
            _notificationService = notificationService;
            _configuration = configuration;
            _s3Client = s3Client;
        }

        [HttpGet("search")]
        public async Task<List<UserInformationResponse>> SearchUser(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            var userRequest = new UserInformationModel()
            {
                FirstName = name,
                LastName = name,
            };
            var users = await _userService.GetUsersByName(userRequest);

            if (users == null)
            {
                return null;
            }

            var newUsers = new List<UserInformationResponse>();

            foreach (var newUser in users)
            {
                var userInfoResponse = new UserInformationResponse()
                {
                    UserId = newUser.UserId,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                };
                newUsers.Add(userInfoResponse);
            }

            return newUsers;
        }

        [HttpGet]
        public async Task<IEnumerable<UserResponse>> Get()
        {
            var userModel = await _userService.GetAllUsers();

            if (userModel == null)
            {
                return null;
            }
            var userResponses = new List<UserResponse>();

            foreach (var user in userModel)
            {
                var newUserResponse = new UserResponse
                {
                    Email = user.Email,
                };
                userResponses.Add(newUserResponse);
            }

            return userResponses;
        }

        [HttpGet("groups")]
        public async Task<IEnumerable<UsersGroupResponse>> GetAllGroups()
        {
            var groupsModel = await _userService.GetAllGroups();

            if (groupsModel == null)
            {
                return null;
            }
            var groups = new List<UsersGroupResponse>();

            foreach (var group in groupsModel)
            {
                var groupResponse = new UsersGroupResponse
                {
                    Id = group.Id,
                    CreatorId = group.CreatorId,
                    GroupName = group.GroupName,
                    Description = group.Description,
                };
                groups.Add(groupResponse);
            }

            return groups;
        }

        [HttpGet("user")]
        public async Task<UserResponse> GetUserbyId(int userId)
        {
            if (userId <= 0)
            {
                return null;
            }
            var user = await _userService.GetUserById(userId);

            if (user == null)
            {
                return null;
            }

            var userResponse = new UserResponse
            {
                Email = user.Email
            };

            return userResponse;
        }

        [HttpGet("group/{groupId}")]
        public async Task<UsersGroupResponse> GetGroupById(int groupId)
        {
            if (groupId <= 0)
            {
                return null;
            }

            var groupModel = await _userService.GetGroupById(groupId);

            if (groupModel == null)
            {
                return null;
            }

            var groupResponse = new UsersGroupResponse
            {
                Id = groupModel.Id,
                CreatorId = groupModel.CreatorId,
                GroupName = groupModel.GroupName,
                Description = groupModel.Description,
            };

            return groupResponse;
        }

        [HttpGet("groupName")]
        public async Task<List<UsersGroupResponse>> GetGroupsByName(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return null;
            }
            var groupModel = await _userService.GetGroupsByName(groupName);

            if (groupModel == null)
            {
                return null;
            }
            var groups = new List<UsersGroupResponse>();
            foreach (var group in groupModel)
            {
                var groupResponse = new UsersGroupResponse
                {
                    Id = group.Id,
                    CreatorId = group.CreatorId,
                    GroupName = group.GroupName,
                    Description = group.Description,
                };
                groups.Add(groupResponse);
            }
            return groups;
        }

        [HttpGet("groups/user")]
        public async Task<List<UsersGroupResponse>> GetGroupsForUser(int userId)
        {
            if (userId <= 0)
            {
                return null;
            }
            var usersGroupModel = await _userService.GetGroupsForUser(userId);

            if (usersGroupModel == null)
            {
                return null;
            }
            return _mapper.Map<List<UsersGroupResponse>>(usersGroupModel);
        }

        [HttpGet("group/users")]
        public async Task<List<UserResponse>> GetUsersByGroupId(int groupId)
        {
            if (groupId <= 0)
            {
                return null;
            }
            var usersModel = await _userService.GetUsersByGroupId(groupId);

            if (usersModel == null)
            {
                return null;
            }

            return _mapper.Map<List<UserModel>, List<UserResponse>>(usersModel);
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
        public async Task<ActionResult> LogOut()
        {
            var user = HttpContext.Items["User"] as UserModel;

            if (user != null)
            {
                user.Token = null;

                return Ok();
            }
            return BadRequest("Not User");
        }

        [HttpGet("byFirstChars")]
        public async Task<ActionResult<List<SearchResponse>>> GetByFirstChars(string nameFirstChars, bool includeGroups)
        {
            if (string.IsNullOrEmpty(nameFirstChars))
            {
                return BadRequest("This field is required!");
            }
            var searchResults = await _userService.GetSearchResult(nameFirstChars, includeGroups);

            if (searchResults == null || !searchResults.Any())
            {
                return NotFound("Users Not Found");
            }

            var searchResponses = _mapper.Map<List<SearchModel>, List<SearchResponse>>(searchResults);

            return Ok(searchResponses);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login([FromBody] LoginRequest loginRequest)
        {
            var userModel = _mapper.Map<LoginRequest, UserModel>(loginRequest);
            var user = await _userService.Login(userModel, _configuration);

            if (user != null)
            {
                if (user.StatusId == (int)StatusEnumModel.Active)
                {
                    return Ok(user);
                }
                return BadRequest("Please check your email");
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
        [Authorize]
        public async Task<ActionResult<int>> UserInfoRegistration([FromBody] UserInformationRequest userInformationRequest)
        {
            var user = HttpContext.Items["User"] as UserModel;

            if (user == null)
            {
                return BadRequest();
            }

            var userInformationModel = new UserInformationModel()
            {
                UserTypeEnum = userInformationRequest.UserTypeEnum,
                UserId = user.Id,
                Gender = userInformationRequest.Gender,
                FirstName = userInformationRequest.FirstName,
                LastName = userInformationRequest.LastName,
                MiddleName = userInformationRequest.MiddleName,
                Metadata = userInformationRequest.UserMetadata,
                PhoneNumber = userInformationRequest.PhoneNumber,
                DateOfBirth = userInformationRequest.DateOfBirth,
            };

            var newUser = await _userService.GetUserById(userInformationModel.UserId);
            var userInfoId = await _userService.UserInformationRegistration(userInformationModel, _configuration);

            var bucketName = _configuration.GetSection("AppSettings:BucketName").Value;
            var template = _configuration.GetSection("AppSettings:Templates").Value;
            var key = "NotificationTemplates/CheckingEmail.html";
            var body = await GetFileByKeyAsync(bucketName, key);

            _notificationService.CheckingEmail(newUser, new NotificationModel(), _configuration, body);

            return Ok(userInfoId);
        }

        [HttpPost("groups")]
        [Authorize]
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

        [HttpPost("groups/addUser")]
        public async Task<ActionResult> AddUserToGroup(int userId, int groupId)
        {
            if (userId <= 0 || groupId <= 0)
            {
                return BadRequest("Process Failed");
            }
            var result = await _userService.AddUserToGroup(userId, groupId);

            if (!result)
            {
                return BadRequest("Process Failed");
            }
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Put([FromBody] UserRequest userRequest)
        {
            var user = HttpContext.Items["User"] as UserModel;

            if (user == null)
            {
                return BadRequest();
            }

            var userModel = _mapper.Map<UserRequest, UserModel>(userRequest);

            var modifyUser = await _userService.ModifyUserCredentials(userModel.Email, userModel.Password, user.Id);

            if (modifyUser != null)
            {
                if (modifyUser.StatusId == (int)StatusEnumModel.Pending)
                {
                    var bucketName = _configuration.GetSection("AppSettings:BucketName").Value;
                    var template = _configuration.GetSection("AppSettings:Templates").Value;
                    var key = "NotificationTemplates/CheckingEmail.html";
                    var body = await GetFileByKeyAsync(bucketName, key);

                    _notificationService.CheckingEmail(modifyUser, new NotificationModel(), _configuration, body);
                }
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("userInfo")]
        public async Task<ActionResult> PutUserInfo([FromBody] UserInformationRequest userInformationRequest)
        {
            var user = HttpContext.Items["User"] as UserModel;

            if (user == null)
            {
                return BadRequest();
            }

            var userInfoModel = new UserInformationModel()
            {
                UserTypeEnum = userInformationRequest.UserTypeEnum,
                UserId = user.Id,
                Gender = userInformationRequest.Gender,
                FirstName = userInformationRequest.FirstName,
                LastName = userInformationRequest.LastName,
                MiddleName = userInformationRequest.MiddleName,
                Metadata = userInformationRequest.UserMetadata,
                PhoneNumber = userInformationRequest.PhoneNumber,
                DateOfBirth = userInformationRequest.DateOfBirth,
            };

            var modifyUser = await _userService.ModifyUserInfo(userInfoModel, user.Id);

            if (modifyUser)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest();
            }
            await _userService.DeleteUser(userId);

            return Ok();
        }
        
        [HttpDelete("groups")]
        public async Task<ActionResult> DeleteUserFromGroup(int userId, int groupId)
        {
            if (userId <= 0 || groupId <= 0)
            {
                return BadRequest("Unable to remove user from group");
            }
            await _userService.DeleteUserFromGroup(userId, groupId);

            return Ok();
        }
        
        [HttpDelete("group")]
        public async Task<ActionResult> DeleteGroup(int groupId)
        {
            if (groupId <= 0)
            {
                return BadRequest("Group not found");
            }
            await _userService.DeleteGroup(groupId);

            return NoContent();
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
