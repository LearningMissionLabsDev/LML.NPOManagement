using Amazon.S3;
using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Request;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        private IUserService _userService;
        private IConfiguration _configuration;
        private INotificationService _notificationService;
        private IAmazonS3 _s3Client;

        public UserController(IUserService userService, IConfiguration configuration, INotificationService notificationService, IAmazonS3 s3Client)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountRequest, AccountModel>();
                cfg.CreateMap<AccountProgressRequest, AccountProgressModel>();
                cfg.CreateMap<AttachmentRequest, AttachmentModel>();
                cfg.CreateMap<DonationRequest, DonationModel>();
                cfg.CreateMap<InventoryTypeRequest, InventoryTypeModel>();
                cfg.CreateMap<InvestorInformationRequest, InvestorInformationModel>();
                cfg.CreateMap<NotificationRequest, NotificationModel>();
                cfg.CreateMap<RoleRequest, RoleModel>();
                cfg.CreateMap<UserInformationRequest, UserInformationModel>();
                cfg.CreateMap<UserInventoryRequest, UserInventoryModel>();
                cfg.CreateMap<UserRequest, UserModel>();
                cfg.CreateMap<AccountModel, AccountResponse>();
                cfg.CreateMap<AccountProgressModel, AccountProgressResponse>();
                cfg.CreateMap<AttachmentModel, AttachmentResponse>();
                cfg.CreateMap<DonationModel, DonationResponse>();
                cfg.CreateMap<InventoryTypeModel, InventoryTypeResponse>();
                cfg.CreateMap<InvestorInformationModel, InvestorInformationResponse>();
                cfg.CreateMap<InvestorTierTypeModel, InvestorTierTypeResponse>();
                //cfg.CreateMap<NotificationModel, NotificationResponse>();
                cfg.CreateMap<NotificationTransportTypeModel, NotificationTypeResponse>();
                cfg.CreateMap<RoleModel, RoleResponse>();
                cfg.CreateMap<UserInformationModel, UserInformationResponse>();
                cfg.CreateMap<UserInventoryModel, UserInventoryResponse>();
                cfg.CreateMap<UserModel, UserResponse>();
                cfg.CreateMap<UserTypeModel, UserTypeResponse>();
                cfg.CreateMap<LoginRequest, UserModel>();
            });
            _mapper = config.CreateMapper();
            _userService = userService;
            _configuration = configuration;
            _notificationService = notificationService;
            _s3Client = s3Client;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<UserResponse>> Get()
        {
            var userModel = await _userService.GetAllUsers();
            return _mapper.Map<List<UserModel>, List<UserResponse>>(userModel);
        }

        //GET: api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<UserResponse> Get(int id)
        {
            var user = await  _userService.GetUserById(id);
            return _mapper.Map<UserModel, UserResponse>(user);
        }

		[HttpGet("byFirstChars")] // Api Endpoint correction ? 
		public async Task<ActionResult<List<UserInformationResponse>>> GetByFirstChars(string firstChars, bool showGroupsOnly)
		{
            var currentUser = HttpContext.Items["User"] as UserModel;
            if (currentUser == null)
            {
                return BadRequest("Current User Null");
            }

            var users = await _userService.GetUserByUsername(firstChars, showGroupsOnly, currentUser.Id);
			if (users == null)
			{
				return NotFound("Users Not Found");
			}


			return Ok(_mapper.Map<List<UserInformationModel>, List<UserInformationResponse>>(users));
		}

		// PUT api/<UserController>/5
		[HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UserRequest userRequest)
        {
            var user = await _userService.GetUserById(id);
            if(user == null)
            {
                return BadRequest();
            }
            var userModel = _mapper.Map<UserRequest, UserModel>(userRequest);
            var modifyUser = await _userService.ModifyUser(user, id);
            if (modifyUser)
            {
                return Ok();
            }
            return BadRequest();
        }

        // PUT api/<UserController>/5
        [HttpPut("userInfo")]
        public async Task<ActionResult> PutUserInfo(int id,[FromBody] UserInformationRequest userInformationRequest)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return BadRequest();
            }
            var userInfoModel = new UserInformationModel()
            {
                UserTypeEnum = userInformationRequest.UserTypeEnum,
                UserId = id,
                Gender = userInformationRequest.Gender,
                FirstName = userInformationRequest.FirstName,
                LastName = userInformationRequest.LastName,
                Metadata = userInformationRequest.UserMetadata,
                PhoneNumber = userInformationRequest.PhoneNumber,
                DateOfBirth = userInformationRequest.DateOfBirth,
            };
            var modifyUser = await _userService.ModifyUserInfo(userInfoModel, id);
            if (modifyUser)
            {
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user != null)
            {
                _userService.DeleteUser(id);
            }
            return BadRequest();
        }

        // Get api/<UserController>       
        [HttpGet("logout")]
        public async Task<ActionResult> LogOut()
        {
            var user = HttpContext.Items["User"] as UserModel;
            if(user != null)
            {
                user.Token = null;
                return Ok();
            }
            return BadRequest("Not User");
        }

        // POST api/<UserController>       
        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login([FromBody] LoginRequest loginRequest)
        {
            var userModel = _mapper.Map<LoginRequest, UserModel>(loginRequest);
            var user = await _userService.Login(userModel, _configuration);
            if (user != null)
            {
                if(user.Status == StatusEnumModel.Activ)
                {
                    return Ok(user);
                }
                return BadRequest("Please check your email");
            }
            return Unauthorized(401);
        }

        // GET: api/<UserController>
        [HttpGet("verifyEmail")]
        public async Task<ActionResult> VerifyEmail([FromQuery] string token)
        {
            var user = await _userService.ActivationUser(token, _configuration);
            var bucketName = _configuration.GetSection("AppSettings:BucketName").Value;
            var template = _configuration.GetSection("AppSettings:Templates").Value;
            var key = template + "RegistracionNotification.html";
            var body = await GetFileByKeyAsync(bucketName, key);
            _notificationService.SendNotificationUserAsync(user, new NotificationModel(),body);
            return Ok();
        }

        // POST api/<UserController> 
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

        // POST api/<UserController> 
        [HttpPost("userInfoRegistration")]
        [Authorize]
        public async Task<ActionResult<int>> UserInfoRegistration( [FromBody] UserInformationRequest userInformationRequest)
        {
            var user = HttpContext.Items["User"] as UserModel ;
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
                Metadata = userInformationRequest.UserMetadata,
                PhoneNumber = userInformationRequest.PhoneNumber,
                DateOfBirth = userInformationRequest.DateOfBirth,
            };
            var newUser = await _userService.GetUserById(userInformationModel.UserId);
            var userInfoId = await _userService.UserInformationRegistration(userInformationModel, _configuration);
            switch (userInformationRequest.UserTypeEnum)
            {
                case UserTypeEnum.Admin:
                    var userType = await _userService.AddUserType(userInformationModel);
                    break;
                case UserTypeEnum.AccountManager:
                    userType =  await _userService.AddUserType(userInformationModel);
                    break;
                case UserTypeEnum.Beneficiary:
                    userType =  await _userService.AddUserType(userInformationModel);
                    break;
                case UserTypeEnum.Investor:
                    userType = await _userService.AddUserType(userInformationModel);
                    break;
                default:
                    break;
            }
            var bucketName = _configuration.GetSection("AppSettings:BucketName").Value;
            var template = _configuration.GetSection("AppSettings:Templates").Value;
            var key = "NotificationTemplates/CheckingEmail.html";
            var body = await GetFileByKeyAsync(bucketName, key);
            
            _notificationService.CheckingEmail(newUser, new NotificationModel(), _configuration, body);
            return Ok(userInfoId);                  
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
