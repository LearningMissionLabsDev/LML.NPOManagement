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
    public class UserInventoryController : ControllerBase
    {
        private IMapper _mapper;
        private IUserInventoryService _userInventoryService;
        private IWebHostEnvironment _webHostEnvironment;
        private INotificationService _notificationService;
        private IUserService _userService;

        public UserInventoryController(IUserInventoryService userInventoryService, IWebHostEnvironment webHostEnvironment,
                                       INotificationService notificationService, IUserService userService)
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
            _userInventoryService = userInventoryService;
            _notificationService = notificationService;
            _webHostEnvironment = webHostEnvironment;
            _notificationService.AppRootPath = _webHostEnvironment.ContentRootPath;
            _userService = userService;
        }

        // GET: api/<UserInventoryController>
        [HttpGet]
        public async Task<IEnumerable<UserInventoryResponse>> Get()
        {
            var inventories = await _userInventoryService.GetAllUserInventories();
            return _mapper.Map<List<UserInventoryModel>,List<UserInventoryResponse>>(inventories);
        }

        // GET api/<UserInventoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult< UserInventoryResponse>> Get(int id)
        {
            var inventory = await _userInventoryService.GetUserInventoryById(id);
            if(inventory == null)
            {
                return BadRequest();
            }
            return Ok(inventory);
        }

        // POST api/<UserInventoryController>
        [HttpPost]
        public async Task<ActionResult<UserInventoryResponse>> Post([FromBody] UserInventoryRequest userInventoryRequest)
        {
            var user = await _userService.GetUserById(userInventoryRequest.UserId);
            if(user == null)
            {
                return BadRequest();
            }
            var inventoryType = await _userInventoryService.GetUserInventoryTypeById(userInventoryRequest.InventoryTypeId);
            if(inventoryType == null)
            {
                return BadRequest();
            }
            var inventoryModel = _mapper.Map<UserInventoryRequest,UserInventoryModel>(userInventoryRequest);
            var inventory = await _userInventoryService.AddUserInventory(inventoryModel);
            var inventoryResponse = _mapper.Map<UserInventoryModel,UserInventoryResponse>(inventory);
            return Ok(inventoryResponse);
        }

        // POST api/<UserInventoryController>
        [HttpPost("inventoryType")]
        public async Task<ActionResult<InventoryTypeResponse>> PostInventoryType([FromBody] InventoryTypeRequest inventoryTypeRequest)
        {
            var inventoryType = _mapper.Map<InventoryTypeRequest,InventoryTypeModel>(inventoryTypeRequest);
            var newInventory = await _userInventoryService.AddInventoryType(inventoryType);
            return Ok(_mapper.Map<InventoryTypeModel,InventoryTypeResponse>(newInventory));
        }

        // PUT api/<UserInventoryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserInventoryResponse>> Put(int id, [FromBody] UserInventoryRequest userInventoryRequest)
        {
            var inventory = _userInventoryService.GetUserInventoryById(id);
            if( inventory == null)
            {
                return BadRequest();
            }
            var user = await _userService.GetUserById(userInventoryRequest.UserId);
            if (user == null)
            {
                return BadRequest();
            }
            var inventoryType = await _userInventoryService.GetUserInventoryTypeById(userInventoryRequest.InventoryTypeId);
            if (inventoryType == null)
            {
                return BadRequest();
            }
            var inventoryModel = _mapper.Map<UserInventoryRequest, UserInventoryModel>(userInventoryRequest);
            var newInventory = await _userInventoryService.ModifyUserInventory(inventoryModel, id);
            return Ok(newInventory);
        }

    }
}
