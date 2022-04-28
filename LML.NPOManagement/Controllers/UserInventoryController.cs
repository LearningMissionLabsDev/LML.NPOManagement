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

        public UserInventoryController(IUserInventoryService userInventoryService, IWebHostEnvironment webHostEnvironment,
                                       INotificationService notificationService)
        {
            var config = new MapperConfiguration(cfg =>
            {
               
                cfg.CreateMap<NotificationRequest, NotificationModel>();
                cfg.CreateMap<UserInventoryRequest, UserInventoryModel>();
                cfg.CreateMap<InventoryTypeModel, InventoryTypeResponse>();
                cfg.CreateMap<NotificationModel, NotificationResponse>();
                cfg.CreateMap<NotificationTransportTypeModel, NotificationTypeResponse>();
                cfg.CreateMap<UserInventoryModel, UserInventoryResponse>();
            });
            _mapper = config.CreateMapper();
            _userInventoryService = userInventoryService;
            _notificationService = notificationService;
            _webHostEnvironment = webHostEnvironment;
            _notificationService.AppRootPath = _webHostEnvironment.ContentRootPath;
        }

        // GET: api/<UserInventoryController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserInventoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserInventoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserInventoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserInventoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
