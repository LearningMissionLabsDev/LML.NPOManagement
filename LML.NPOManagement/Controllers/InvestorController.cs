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
    public class InvestorController : ControllerBase
    {
        private IMapper _mapper;
        private IInvestorService _investorInformationService;
        private INotificationService _notificationService;
        private IWebHostEnvironment _webHostEnvironment;
        public InvestorController(IInvestorService investorInformationService, INotificationService notificationService,
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
                cfg.CreateMap<InvestorTierTypeRequest, InvestorTierTypeModel>();
                cfg.CreateMap<MeetingScheduleRequest, MeetingScheduleModel>();
                cfg.CreateMap<NotificationRequest, NotificationModel>();
                cfg.CreateMap<NotificationTypeRequest, NotificationTypeModel>();
                cfg.CreateMap<RoleRequest, RoleModel>();
                cfg.CreateMap<TemplateRequest, TemplateModel>();
                cfg.CreateMap<TemplateTypeRequest, TemplateTypeModel>();
                cfg.CreateMap<UserInformationRequest, UserInformationModel>();
                cfg.CreateMap<UserInventoryRequest, UserInventoryModel>();
                cfg.CreateMap<UserRequest, UserModel>();
                cfg.CreateMap<UserTypeRequest, UserTypeModel>();
                cfg.CreateMap<WeeklyScheduleRequest, WeeklyScheduleModel>();
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
                cfg.CreateMap<NotificationTypeModel, NotificationTypeResponse>();
                cfg.CreateMap<RoleModel, RoleResponse>();
                cfg.CreateMap<TemplateModel, TemplateResponse>();
                cfg.CreateMap<TemplateTypeModel, TemplateTypeResponse>();
                cfg.CreateMap<UserInformationModel, UserInformationResponse>();
                cfg.CreateMap<UserInventoryModel, UserInventoryResponse>();
                cfg.CreateMap<UserModel, UserResponse>();
                cfg.CreateMap<UserTypeModel, UserTypeResponse>();
                cfg.CreateMap<WeeklyScheduleModel, WeeklyScheduleResponse>();

            });
            _mapper = config.CreateMapper();
            _investorInformationService = investorInformationService;
            _notificationService = notificationService;
            _webHostEnvironment = webHostEnvironment;
            _notificationService.AppRootPath = _webHostEnvironment.ContentRootPath;
        }
        // GET: api/<InvestorInformationController>
        [HttpGet]
        public IEnumerable<InvestorInformationResponse> GetAllInvestorInformations()
        {
            var investor = _investorInformationService.GetAllInvestorInformations().ToList();
            return _mapper.Map<List<InvestorInformationModel>, List<InvestorInformationResponse>>(investor);
        }

        // GET api/<InvestorInformationController>/5
        [HttpGet("{id}")]
        public InvestorInformationResponse GetInvestorInformationById(int id)
        {
            var investor = _investorInformationService.GetInvestorInformationById(id);
            return _mapper.Map<InvestorInformationModel, InvestorInformationResponse>(investor);
        }

        // GET: api/<InvestorInformationController>
        [HttpGet("donation")]
        public IEnumerable<DonationResponse> GetAllDonation()
        {
            var donation = _investorInformationService.GetAllDonation().ToList();
            return _mapper.Map<List<DonationModel>,List<DonationResponse>>(donation);
        }

        // GET api/<InvestorInformationController>/5
        [HttpGet("getDonationByid")]
        public DonationResponse GetDonationById(int id)
        {
            var donation = _investorInformationService.GetDonationById(id);
            return _mapper.Map<DonationModel,DonationResponse>(donation);
        }

        // GET api/<InvestorInformationController>/5
        [HttpGet("year")]
        public string GetDonationByYear(DateTime dateTime)
        {
            return "value";
        }

        // POST api/<InvestorInformationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<InvestorInformationController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] DonationRequest donationRequest)
        {
            var donation = _mapper.Map<DonationRequest, DonationModel>(donationRequest);
            var modifyDonation = _investorInformationService.ModifyDonation(donation, id);
            if (modifyDonation != null)
            {
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<InvestorInformationController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var donation = _investorInformationService.GetDonationById(id);
            if (donation != null)
            {
                _investorInformationService.DeleteDonation(id);
            }

            return BadRequest();
        }
    }
}
