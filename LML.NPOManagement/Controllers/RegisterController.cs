using Amazon.S3;
using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Request;
using Microsoft.AspNetCore.Mvc;

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private IMapper _mapper;
        private IAmazonS3 _s3Client;
        private IRegistrationService _registrationService;
        private IUserService _userService;
        private IConfiguration _configuration;
        private INotificationService _notificationService;

        public RegisterController(IRegistrationService registrationService,IUserService userService,INotificationService notificationService, IAmazonS3 amazonS3, IConfiguration configuration)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LoginRequest, UserModel>();
                cfg.CreateMap<UserRequest, UserModel>();
            });
            _mapper = config.CreateMapper();
            _registrationService = registrationService;
            _notificationService = notificationService;
            _userService = userService;
            _configuration = configuration;
            _s3Client = amazonS3;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login([FromBody] LoginRequest loginRequest)
        {
            var userModel = _mapper.Map<LoginRequest, UserModel>(loginRequest);
            var user = await _registrationService.Login(userModel, _configuration);
            if (user != null)
            {
                if (user.Status == StatusEnumModel.Activ)
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
            var result = await _registrationService.Registration(userModel, _configuration);
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
                Metadata = userInformationRequest.UserMetadata,
                PhoneNumber = userInformationRequest.PhoneNumber,
                DateOfBirth = userInformationRequest.DateOfBirth,
            };
            var newUser = await _userService.GetUserById(userInformationModel.UserId);
            var userInfoId = await _registrationService.UserInformationRegistration(userInformationModel, _configuration);
            switch (userInformationRequest.UserTypeEnum)
            {
                case UserTypeEnum.Admin:
                    var userType = await _userService.AddUserType(userInformationModel);
                    break;
                case UserTypeEnum.AccountManager:
                    userType = await _userService.AddUserType(userInformationModel);
                    break;
                case UserTypeEnum.Beneficiary:
                    userType = await _userService.AddUserType(userInformationModel);
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

        [HttpGet("verifyEmail")]
        public async Task<ActionResult> VerifyEmail([FromQuery] string token)
        {
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
