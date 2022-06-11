using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Request;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private IMapper _mapper;
        private INotificationService _notificationService;
        private IUserService _userService;
        private  IWebHostEnvironment _webHostEnvironment;

        public NotificationController(INotificationService notificationService, IUserService userService,
                                      IWebHostEnvironment webHostEnvironment)
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
            _notificationService = notificationService;            
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
            _notificationService.AppRootPath = _webHostEnvironment.ContentRootPath;
        }        

        // GET: api/<NotificationController>
        [HttpGet]
        public async Task<List<NotificationResponse>> Get()
        {
            var notification = await _notificationService.GetAllNotifications();
            return _mapper.Map<List<NotificationModel>, List<NotificationResponse>>(notification);
        }
        
        // GET api/<NotificationController>/5
        [HttpGet("{id}")]
        public async Task<NotificationResponse> GetNotificationById(int id)
        {
            var notification = await _notificationService.GetNotificationById(id);
            return _mapper.Map<NotificationModel,NotificationResponse>(notification);
        }

        // POST api/<NotificationController>
        [HttpPost]
        public async Task<ActionResult> Post(int id, [FromBody] NotificationRequest notificationRequest)
        {
            var notification =  _mapper.Map<NotificationRequest, NotificationModel>(notificationRequest);

            switch (notificationRequest.NotificationContext)
            {
                case NotificationContext.Users:

                    var users = await  _userService.GetAllUsers();
                    _notificationService.SendNotifications(users, notification);
                    return Ok();
                    
                case NotificationContext.Account:

                    var userByAccounts = await  _userService.GetUsersByAccount(id);
                    _notificationService.SendNotifications(userByAccounts, notification);
                    return Ok();
                    
                case NotificationContext.Role:

                    var userByRoles = await _userService.GetUsersByRole(id);
                    _notificationService.SendNotifications(userByRoles, notification);
                    return Ok();
                   
                case NotificationContext.InvestorTier:

                    var userByInvestorTier = await _userService.GetUsersByInvestorTier(id);
                     _notificationService.SendNotifications(userByInvestorTier, notification);
                    return Ok();
                   
                default:
                    return BadRequest();                    
            }           
        }

        // PUT api/<NotificationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NotificationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}