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
    public class AccountController : ControllerBase
    {
        private IMapper _mapper;
        private IAccountService _accountService;
        private IWebHostEnvironment _webHostEnvironment;
        private INotificationService _notificationService;
        private IUserService _userService;
        public AccountController(IAccountService accountService, IWebHostEnvironment webHostEnvironment,
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
                cfg.CreateMap<UserIdeaRequest, UserIdeaModel>();
                cfg.CreateMap<UserIdeaModel, UserIdeaResponse>();
            });
            _mapper = config.CreateMapper();
            _accountService = accountService;
            _webHostEnvironment = webHostEnvironment;
            _notificationService = notificationService;
            _notificationService.AppRootPath = _webHostEnvironment.ContentRootPath;
            _userService = userService;
        }

        // GET: api/<AccountController>
        [HttpGet]
        public async Task<List<AccountResponse>> Get()
        {
            var accounts = await _accountService.GetAllAccounts();
            return _mapper.Map<List<AccountModel>,List<AccountResponse>>(accounts);
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<AccountResponse> Get(int id)
        {
            var account = await _accountService.GetAccountById(id);
            return _mapper.Map<AccountModel,AccountResponse>(account);
        }

        // GET: api/<AccountController>
        [HttpGet("idea")]
        public async Task<List<UserIdeaResponse>> GetIdea()
        {
            List<UserIdeaResponse> ideaModels = new List<UserIdeaResponse>();
            var ideas = await _accountService.GetAllIdea();
            if (ideas.Count > 0)
            {
                foreach (var idea in ideas)
                {
                    var ideaResponse = _mapper.Map<UserIdeaModel, UserIdeaResponse>(idea);
                    ideaModels.Add(ideaResponse);
                }
                return ideaModels;
            }
            return null;
        }

        // POST api/<AccountController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // POST api/<AccountController>
        [HttpPost("submitComments")]
        public async Task<ActionResult> SubmitComments([FromBody] UserIdeaRequest userIdeaRequest)
        {
            var user = _userService.GetUserById(userIdeaRequest.UserId);
            if(user == null)
            {
                return BadRequest();
            }
            var ideaModel = _mapper.Map<UserIdeaRequest, UserIdeaModel>(userIdeaRequest);
            var idea = await _accountService.AddUserIdea(ideaModel);
            return Ok();
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _accountService.DeleteAccount(id);
        }
    }
}
