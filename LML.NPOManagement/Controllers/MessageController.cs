using Amazon.S3;
using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Request;
using LML.NPOManagement.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMapper _mapper;
        private IMessageService _messageService;
        private IConfiguration _configuration;
        private INotificationService _notificationService;
        private IAmazonS3 _s3Client;

        public MessageController(IMessageService messageService, IConfiguration configuration, INotificationService notificationService, IAmazonS3 s3Client)
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
                cfg.CreateMap<MessageModel, MessageResponse>(); // 1
                cfg.CreateMap<MessageRequest, MessageModel>(); // 2
                cfg.CreateMap<UserTypeModel, UserTypeResponse>();
                cfg.CreateMap<LoginRequest, UserModel>();
            });
            _mapper = config.CreateMapper();
            _messageService = messageService;
            _configuration = configuration;
            _notificationService = notificationService;
            _s3Client = s3Client;
        }

        [HttpPost]
        public async Task<ActionResult<MessageResponse>> AddMessage([FromBody] MessageRequest messageRequest)
        {
            var messageModel = _mapper.Map<MessageRequest, MessageModel>(messageRequest);
            var message = await _messageService.AddMessage(messageModel);
            if (message != null)
            {
                var messageResponse = _mapper.Map<MessageModel, MessageResponse>(message);
                return Ok(messageResponse);
            }

            return BadRequest();
        }

        [HttpGet("{username}/{recovery}")]
        public async Task<ActionResult<List<MessageModel>>> GetMessages(string username, string recovery)
        {
            var messageModels = await _messageService.GetMessages(username, recovery);
            return Ok(_mapper.Map<List<MessageModel>, List<MessageResponse>>(messageModels));
        }
    }
}