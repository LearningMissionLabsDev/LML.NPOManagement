using AutoMapper;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;
using LML.NPOManagement.Request;
using LML.NPOManagement.Bll.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private IMapper _mapper;
        private IDonationService _donationService;
        public DonationController(IDonationService donationService)
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
                cfg.CreateMap<InvestorTierRequest, InvestorTierModel>();
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
                cfg.CreateMap<InvestorTierModel, InvestorTierResponse>();
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
            _donationService = donationService;
        }
        // GET: api/<DonationController>
        [HttpGet]
        public IEnumerable<DonationResponse> Get()
        {
            var donations = _donationService.GetAllDonations().ToList();
            return _mapper.Map<List<DonationModel>,List<DonationResponse>>(donations);
        }

        // GET api/<DonationController>/5
        [HttpGet("{id}")]
        public DonationResponse Get(int id)
        {
            var donation = _donationService.GetDonationById(id);
            return _mapper.Map<DonationModel, DonationResponse>(donation);
        }

        // POST api/<DonationController>
        [HttpPost]
        public async Task<DonationResponse> Post([FromBody] DonationRequest donationRequest)
        {
            var addDonation = _mapper.Map<DonationRequest, DonationModel>(donationRequest);
            var id = _donationService.AddDonation(addDonation);
            var donationModel = _donationService.GetDonationById(id);
            return _mapper.Map<DonationModel,DonationResponse>(donationModel);
        }

        // PUT api/<DonationController>/5
        [HttpPut("{id}")]
        public async Task<DonationResponse> Put(int id, [FromBody] DonationRequest donationRequest)
        {
            //...RG We need to convert the calls to asynch inside those methods. Currently all calls are synchronous.
            //... todo: For that we need to convert the BLL methods to asynchronous as well. To be discussed.
            var modifyDonation = _mapper.Map<DonationRequest,DonationModel>(donationRequest);
            var donationId = _donationService.ModifyDonation(modifyDonation, id);
            var donationModel = _donationService.GetDonationById(donationId);
            return _mapper.Map<DonationModel, DonationResponse>(donationModel);
        }

        // DELETE api/<DonationController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }
            var donationToDelete = _donationService.GetDonationById(id);
            if(donationToDelete == null)
            {
                return NotFound();           
            }
            _donationService.DeleteDonation(id);

            return Ok();
            
        }
    }
}
