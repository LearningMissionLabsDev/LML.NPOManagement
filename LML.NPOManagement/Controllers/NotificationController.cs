using Amazon.S3;
using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Request;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private IMapper _mapper;
        private INotificationService _notificationService;
        private IAccountService _accountService;
        private IUserService _userService;
        private IAmazonS3 _s3Client;
        private IConfiguration _configuration;

        public NotificationController(INotificationService notificationService, IUserService userService, IAccountService accountService,
                                      IAmazonS3 s3Client, IConfiguration configuration)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountRequest, AccountModel>();
                cfg.CreateMap<AttachmentRequest, AttachmentModel>();
                cfg.CreateMap<DonationRequest, DonationModel>();
                cfg.CreateMap<InventoryTypeRequest, InventoryTypeModel>();
                cfg.CreateMap<InvestorInformationRequest, InvestorInformationModel>();
                cfg.CreateMap<NotificationRequest, NotificationModel>();
                cfg.CreateMap<UserInformationRequest, UserInformationModel>();
                cfg.CreateMap<UserInventoryRequest, UserInventoryModel>();
                cfg.CreateMap<UserRequest, UserModel>();
                cfg.CreateMap<AccountModel, AccountResponse>();
                cfg.CreateMap<AttachmentModel, AttachmentResponse>();
                cfg.CreateMap<DonationModel, DonationResponse>();
                cfg.CreateMap<InventoryTypeModel, InventoryTypeResponse>();
                cfg.CreateMap<InvestorInformationModel, InvestorInformationResponse>();
                cfg.CreateMap<InvestorTierTypeModel, InvestorTierTypeResponse>();
                cfg.CreateMap<NotificationModel, NotificationResponse>();
                cfg.CreateMap<NotificationTransportTypeModel, NotificationTypeResponse>();
                cfg.CreateMap<UserInformationModel, UserInformationResponse>();
                cfg.CreateMap<UserInventoryModel, UserInventoryResponse>();
                cfg.CreateMap<UserModel, UserResponse>();
                cfg.CreateMap<RequestedUserTypeModel, RequestedUserTypeResponse>();
                cfg.CreateMap<LoginRequest, UserModel>();
            });
            _mapper = config.CreateMapper();
            _notificationService = notificationService;  
            _accountService = accountService;
            _userService = userService;
            _s3Client = s3Client;
            _configuration = configuration;
        }      

        // GET: api/<NotificationController>
        [HttpGet]
        [Authorize(RoleAccess.AccountAdmin)]
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
            return _mapper.Map<NotificationModel, NotificationResponse>(notification);
        }

        // GET api/<NotificationController>/5
        [HttpGet("templateType")]
        public async Task<string> GetNotificationByTemplateType(int id)
        {
            return null;
        }

        // POST api/<NotificationController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NotificationRequest notificationRequest)
        {                
            var bucketName = _configuration.GetSection("AppSettings:BucketName").Value;
            var template = _configuration.GetSection("AppSettings:Templates").Value;
            var key = template + notificationRequest.Language.ToString() + notificationRequest.BodyName;
            var body = await GetFileByKeyAsync(bucketName, key);

            switch (notificationRequest.NotificationTransportEnum)
            {             
                case NotificationTransportEnum.Email:
                    var notification = _mapper.Map<NotificationRequest, NotificationModel>(notificationRequest);
                    var notificationModel = _notificationService.AddNotification(notification);
                    break;
                
                case NotificationTransportEnum.Sms:
                    break;
                
                case NotificationTransportEnum.Post:
                    break;
  
                case NotificationTransportEnum.Other:
                    break;
            }
            return Ok();
        }    

        // POST api/<NotificationController>
        [HttpPost("send")]
        public async Task<ActionResult> SendNotification(int id, [FromBody] NotificationRequest notificationRequest )
        {
            var notificationModel = await _notificationService.GetNotificationById(id);
            if (notificationModel == null)
            {
                return BadRequest();
            }
            var notification = _mapper.Map<NotificationRequest, NotificationModel>(notificationRequest);
            var modifyNotification = await _notificationService.ModifyNotification(notification, id);

            switch (notificationModel.NotificationTypeEnum  )
            {
                case NotificationTypeEnum.ByIndividuals:
                    var users = await _userService.GetAllUsers();
                    //_notificationService.SendNotifications(users, modifyNotification);
                    return Ok();
                case NotificationTypeEnum.ByAccounts:

                    var userByAccounts = await _accountService.GetUsersByAccount(id);
                    //_notificationService.SendNotifications(userByAccounts, modifyNotification);
                    return Ok();

                case NotificationTypeEnum.ByRoles:

                    //_notificationService.SendNotifications(userByRoles, modifyNotification);
                    return Ok();

                case NotificationTypeEnum.ByInvestors:

                    var userByInvestorTier = await _userService.GetUsersByInvestorTier(id);
                    //_notificationService.SendNotifications(userByInvestorTier, modifyNotification);
                    return Ok();

                default:
                    return BadRequest();
            }
        }      

        // PUT api/<NotificationController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] NotificationRequest notificationRequest)
        {
            var notification = await _notificationService.GetNotificationById(id);
            if(notification == null)
            {
                return BadRequest();
            }
            var notificationModel = _mapper.Map<NotificationRequest, NotificationModel>(notificationRequest);
            var modifyNotification = await _notificationService.ModifyNotification(notificationModel, id);
            if (modifyNotification != null)
            {
                return Ok(modifyNotification);
            }
            return BadRequest();
        }

        // DELETE api/<NotificationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var notification = await _notificationService.GetNotificationById(id);
            if (notification != null)
            {
                _notificationService.DeleteNotification(id);
                return Ok();
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