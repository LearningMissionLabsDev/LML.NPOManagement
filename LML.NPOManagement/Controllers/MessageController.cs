using Amazon.S3;
using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Request;
using Microsoft.AspNetCore.Mvc;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Response;

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMapper _mapper;
        private IMessageService _messageService;
        private IConfiguration _configuration;
        private ISecurityService _securityService;

        public MessageController(IConfiguration configuration, IMessageService messageService, ISecurityService securityService)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MessageRequest, MessageModel>();
                cfg.CreateMap<MessageModel, MessageResponse>();
            });
            _mapper = config.CreateMapper();
            _messageService = messageService;
            _configuration = configuration;
            _securityService = securityService;
        }

        private ConnectionInformation GetConnectionInformatin()
        {
            return new ConnectionInformation
            {
                Id = Request.HttpContext.Connection.Id,
                IP = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                Port = Request.HttpContext.Connection.RemotePort
            };
        }

        [HttpGet("NewMessagesCount")]
        public async Task<ActionResult<int>> NewMessagesCount()
        {
            var user = HttpContext.Items["User"] as UserModel;
            int count = await _messageService.NewMessagesCount(user.Id);
            return Ok(count);
        }

        [HttpGet("getKey")]
        public async Task<ActionResult<string>> GetKey()
        {
            var connectInfo = GetConnectionInformatin();
            var publicRsaKey = await _securityService.CreateNewKey(connectInfo);
            if (publicRsaKey == null)
            {
                return BadRequest();
            }
            return Ok(publicRsaKey);
        }

        [HttpGet("getMyMessages")]
        [Authorize]
        public async Task<ActionResult<List<MessageResponse>>> GetMyMessages()
        {
            var user = HttpContext.Items["User"] as UserModel;
            var messageModels = await _messageService.GetMyMessages(user.Id);
            List<MessageResponse> messages = new List<MessageResponse>();
            foreach (var model in messageModels)
            {
                var messageResponse = _mapper.Map<MessageModel, MessageResponse>(model);
                messageResponse.Message = null!;
                messages.Add(messageResponse);
            }
            return messages;
        }

        [HttpGet("getMessageById")]
        [Authorize]
        public async Task<ActionResult<MessageResponse>> GetMessageById(int Id)
        {
            var user = HttpContext.Items["User"] as UserModel;
            var messageModel = await _messageService.GetMessageById(user.Id, Id);
            var message = _mapper.Map<MessageModel, MessageResponse>(messageModel);
            if (message == null)
            { 
                return BadRequest();
            }
            return Ok(message);
        }

        [HttpPost("getSecretMessage")]
        [Authorize]
        public async Task<ActionResult<MessageResponse>> GetSecretMessage([FromBody] SecretMessageRequest requesst)
        {
            var user = HttpContext.Items["User"] as UserModel;
            var connectInfo = GetConnectionInformatin();
            var messageModel = await _messageService.GetSecretMessage(user.Id, requesst.messageId, requesst.password, requesst.publicKey, connectInfo);
            var message = _mapper.Map<MessageModel, MessageResponse>(messageModel);
            if (message == null)
            {
                return BadRequest();
            }
            return Ok(message);
        }

        [HttpPost("sendMessage")]
        [Authorize]
        public async Task<ActionResult<string>> SendMessage([FromBody] MessageRequest messageRequest)
        {
            var sender = HttpContext.Items["User"] as UserModel;
            var connectInfo = GetConnectionInformatin();
            var messageModel = _mapper.Map<MessageRequest, MessageModel>(messageRequest);
            messageModel.SenderId = sender.Id;
            bool isSaved = await _messageService.AddMessage(messageModel, connectInfo);
            if (!isSaved)
            {
                return StatusCode(666, "Save error");
            }
            return Ok("Message saved!");
        }
    }
}
