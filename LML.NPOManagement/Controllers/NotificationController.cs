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
                cfg.CreateMap<NotificationRequest, NotificationModel>();
                cfg.CreateMap<NotificationModel, NotificationResponse>();
                cfg.CreateMap<NotificationTransportTypeModel, NotificationTypeResponse>();
            });
            _mapper = config.CreateMapper();
            _notificationService = notificationService;            
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
            _notificationService.AppRootPath = _webHostEnvironment.ContentRootPath;
        }        

        // GET: api/<NotificationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        
        // GET api/<NotificationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NotificationController>
        [HttpPost]
        public async Task<ActionResult> Post(int id, [FromBody] NotificationRequest notificationRequest)
        {
            var notification =  _mapper.Map<NotificationRequest, NotificationModel>(notificationRequest);

            switch (notificationRequest.NotificationContext)
            {
                case NotificationContext.Users:

                    var users =  _userService.GetAllUsers().ToList();
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