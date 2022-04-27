using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Request;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

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
        private IWebHostEnvironment _webHostEnvironment;

        public UserController(IUserService userService, IConfiguration configuration,
            INotificationService notificationService, IWebHostEnvironment webHostEnvironment)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountRequest, AccountModel>();
                cfg.CreateMap<AccountProgressRequest, AccountProgressModel>();
                cfg.CreateMap<AttachmentRequest, AttachmentModel>();
                cfg.CreateMap<DailyScheduleRequest, DailyScheduleModel>();
                cfg.CreateMap<DonationRequest, DonationModel>();
                cfg.CreateMap<InventoryTypeRequest, InventoryTypeModel>();
                cfg.CreateMap<InvestorInformationRequest, InvestorInformationModel>();
                cfg.CreateMap<MeetingScheduleRequest, MeetingScheduleModel>();
                cfg.CreateMap<NotificationRequest, NotificationModel>();
                cfg.CreateMap<RoleRequest, RoleModel>();
                cfg.CreateMap<TemplateRequest, TemplateModel>();
                cfg.CreateMap<TemplateTypeRequest, TemplateTypeModel>();
                cfg.CreateMap<UserInformationRequest, UserInformationModel>();
                cfg.CreateMap<UserInventoryRequest, UserInventoryModel>();
                cfg.CreateMap<UserRequest, UserModel>();
                cfg.CreateMap<AccountModel, AccountResponse>();
                cfg.CreateMap<AccountProgressModel, AccountProgressResponse>();
                cfg.CreateMap<AttachmentModel, AttachmentResponse>();
                cfg.CreateMap<DailyScheduleModel, DailyScheduleResponse>();
                cfg.CreateMap<DonationModel, DonationResponse>();
                cfg.CreateMap<InventoryTypeModel, InventoryTypeResponse>();
                cfg.CreateMap<InvestorInformationModel, InvestorInformationResponse>();
                cfg.CreateMap<InvestorTierTypeModel, InvestorTierTypeResponse>();
                cfg.CreateMap<MeetingScheduleModel, MeetingScheduleResponse>();
                cfg.CreateMap<NotificationModel, NotificationResponse>();
                cfg.CreateMap<NotificationTransportTypeModel, NotificationTypeResponse>();
                cfg.CreateMap<RoleModel, RoleResponse>();
                cfg.CreateMap<TemplateModel, TemplateResponse>();
                cfg.CreateMap<TemplateTypeModel, TemplateTypeResponse>();
                cfg.CreateMap<UserInformationModel, UserInformationResponse>();
                cfg.CreateMap<UserInventoryModel, UserInventoryResponse>();
                cfg.CreateMap<UserModel, UserResponse>();
                cfg.CreateMap<UserTypeModel, UserTypeResponse>();
                cfg.CreateMap<WeeklyScheduleModel, WeeklyScheduleResponse>();
                cfg.CreateMap<LoginRequest, UserModel>();

            });
            _mapper = config.CreateMapper();
            _userService = userService;
            _configuration = configuration;
            _notificationService = notificationService;
            _webHostEnvironment = webHostEnvironment;
            _notificationService.AppRootPath = _webHostEnvironment.ContentRootPath;
        }
        // GET: api/<UserController>
        [HttpGet]
        [Authorize(UserTypeEnum.Investor)]
        public IEnumerable<UserResponse> Get()
        {
            var userModel = _userService.GetAllUsers().ToList();
            return _mapper.Map<List<UserModel>,List<UserResponse>>(userModel);
        }

        // GET: api/<UserController>
        [HttpGet("userTypes")]
        public IEnumerable<UserTypeResponse> GetUserTypes()//return user type table id,description 
        {
            var userTypes = _userService.GetAllUserTypes().ToList();
            return _mapper.Map<List<UserTypeModel>,List<UserTypeResponse>>(userTypes);
        }

        //GET api/<UserController>/5
        [HttpGet("{id}")]
        public UserResponse Get(int id)
        {
            var user = _userService.GetUserById(id);
            return _mapper.Map<UserModel,UserResponse>(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Put(int id, [FromBody] UserRequest userRequest)
        {
            var user = _mapper.Map<UserRequest,UserModel>(userRequest);
            var modifyUser = _userService.ModifyUser(user, id);
            if (modifyUser)
            {
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetUserById(id);
            if(user != null)
            {
                _userService.DeleteUser(id);
            }
            return BadRequest();
        }
        

        // POST api/<UserController>       

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login([FromBody] LoginRequest loginRequest)
        {
            var userModel = _mapper.Map<LoginRequest, UserModel>(loginRequest);
            return await _userService.Login(userModel, _configuration);
        }


        [HttpPost("registration")]
        public async Task<ActionResult> Registration([FromBody] UserRequest userRequest)
        {
            if(userRequest.Password != userRequest.ConfirmPassword)
            {
                return StatusCode(409);
            }
            var userModel = _mapper.Map<UserRequest, UserModel>(userRequest);
            var result = await _userService.Registration(userModel, _configuration);
            
            if (result != null)
            {
                return Ok(result); 
            }
            return StatusCode(409);
        }



        [HttpPost("userInfoRegistration")]
        [Authorize]
        public async Task<ActionResult<int>> UserInfoRegistration([FromBody] UserInformationRequest userInformationRequest)
        {
            var user = HttpContext.Items["User"] as UserModel ;
            userInformationRequest.UserId = user.Id;
            var userInformationModel = _mapper.Map<UserInformationRequest, UserInformationModel>(userInformationRequest);
            var newUser = _userService.GetUserById(userInformationModel.UserId);
            var userInfoId = await _userService.UserInformationRegistration(userInformationModel, _configuration);
            switch (userInformationRequest.UserTypeEnum)
            {
                case UserTypeEnum.Undefined:
                   
                    break;
                case UserTypeEnum.Admin:
                    _userService.AddUserType(userInformationModel);
                    break;
                case UserTypeEnum.AccountManager:
                    _userService.AddUserType(userInformationModel);
                    break;
                case UserTypeEnum.Beneficiary:
                    _userService.AddUserType(userInformationModel);
                    break;
                case UserTypeEnum.Investor:
                    _userService.AddUserType(userInformationModel);
                    break;
                default:
                    break;
            }
            _notificationService.SendNotificationUser(newUser,new NotificationModel());
            return userInfoId;          
        }
    }
}
