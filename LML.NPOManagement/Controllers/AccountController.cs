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
        public AccountController(IAccountService accountService, IWebHostEnvironment webHostEnvironment,
                                INotificationService notificationService)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountRequest, AccountModel>();
                cfg.CreateMap<AccountProgressRequest, AccountProgressModel>();
                cfg.CreateMap<AccountModel, AccountResponse>();
                cfg.CreateMap<AccountProgressModel, AccountProgressResponse>();
            });
            _mapper = config.CreateMapper();
            _accountService = accountService;
            _webHostEnvironment = webHostEnvironment;
            _notificationService = notificationService;
            _notificationService.AppRootPath = _webHostEnvironment.ContentRootPath;
        }

        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<AccountResponse> Get()
        {
            var accounts = _accountService.GetAllAccounts().ToList();
            return _mapper.Map<List<AccountModel>,List<AccountResponse>>(accounts);
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public AccountResponse Get(int id)
        {
            var account = _accountService.GetAccountById(id);
            return _mapper.Map<AccountModel,AccountResponse>(account);
        }

        // POST api/<AccountController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
