using Amazon.S3;
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
        private IConfiguration _configuration;
        private IAmazonS3 _s3Client;
        public InvestorController(IInvestorService investorInformationService, INotificationService notificationService,
                                  IConfiguration configuration, IAmazonS3 s3Client)
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
                cfg.CreateMap<NotificationModel, NotificationResponse>();
                cfg.CreateMap<NotificationTransportTypeModel, NotificationTypeResponse>();
                cfg.CreateMap<RoleModel, RoleResponse>();
                cfg.CreateMap<UserInformationModel, UserInformationResponse>();
                cfg.CreateMap<UserInventoryModel, UserInventoryResponse>();
                cfg.CreateMap<UserModel, UserResponse>();
                cfg.CreateMap<UserTypeModel, UserTypeResponse>();
                cfg.CreateMap<LoginRequest, UserModel>();

            });
            _mapper = config.CreateMapper();
            _investorInformationService = investorInformationService;
            _notificationService = notificationService;
            _configuration = configuration;
            _s3Client = s3Client;
        }

        // GET: api/<InvestorInformationController>
        [HttpGet]
        public async Task<IEnumerable<InvestorInformationResponse>> GetAllInvestorInformations()
        {
            var investor = await _investorInformationService.GetAllInvestorInformations();
            return  _mapper.Map<List<InvestorInformationModel>, List<InvestorInformationResponse>>(investor);
           
        }

        // GET api/<InvestorInformationController>/5
        [HttpGet("{id}")]
        public async Task<InvestorInformationResponse> GetInvestorInformationById(int id)
        {
            var investor = await _investorInformationService.GetInvestorInformationById(id);
            return _mapper.Map<InvestorInformationModel, InvestorInformationResponse>(investor);
          
        }

        // GET: api/<InvestorInformationController>
        [HttpGet("donation")]
        public async Task<IEnumerable<DonationResponse>> GetAllDonation()
        {
            var donation = await _investorInformationService.GetAllDonation();
            return _mapper.Map<List<DonationModel>,List<DonationResponse>>(donation);
        }

        // GET api/<InvestorInformationController>/5
        [HttpGet("getDonationByid")]
        public async Task<DonationResponse> GetDonationById(int id)
        {
            var donation = await _investorInformationService.GetDonationById(id);
            return _mapper.Map<DonationModel,DonationResponse>(donation);
        }

        // GET api/<InvestorInformationController>/5
        [HttpGet("year")]
        public async Task<string> GetDonationByYear(DateTime dateTime)
        {
            return "value";
        }

        // POST api/<InvestorInformationController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] DonationRequest donationRequest)
        {
            var investor = await _investorInformationService.GetInvestorInformationById(donationRequest.InvestorId);
            if(investor == null)
            {
                return StatusCode(409);
            }
            var donationModel = _mapper.Map<DonationRequest,DonationModel>(donationRequest);
            var result = await _investorInformationService.AddDonation(donationModel);
            var bucketName = _configuration.GetSection("AppSettings:BucketName").Value;
            var template = _configuration.GetSection("AppSettings:Templates").Value;
            var key = template + "DonationNotification.html";
            var body = await GetFileByKeyAsync(bucketName, key);
            _notificationService.SendNotificationInvestor(donationModel, new NotificationModel(), body);
            return Ok(result);
        }

        // PUT api/<InvestorInformationController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DonationRequest donationRequest)
        {
            var donation =  _mapper.Map<DonationRequest, DonationModel>(donationRequest);
            var modifyDonation = await _investorInformationService.ModifyDonation(donation, id);
            if (modifyDonation != null)
            {
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<InvestorInformationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var donation = await _investorInformationService.GetDonationById(id);
            if (donation != null)
            {
                _investorInformationService.DeleteDonation(id);
            }
            return BadRequest();
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
