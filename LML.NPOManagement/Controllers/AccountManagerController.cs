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
    public class AccountManagerController : ControllerBase
    {
        private IMapper _mapper;
        private IAccountManagerService _accountManagerService ;

        public AccountManagerController(IAccountManagerService accountManagerService)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountManagerInfoRequest, AccountManagerInfoModel>();
                cfg.CreateMap<BeneficiaryRequest, BeneficiaryModel>();
                cfg.CreateMap<BeneficiaryRoleRequest, BeneficiaryRoleModel>();
                cfg.CreateMap<StatusRequest, StatusModel>();
                cfg.CreateMap<AccountManagerRequest, AccountManagerModel>();
                cfg.CreateMap<AccountManagerRoleRequest, AccountManagerRoleModel>();
                cfg.CreateMap<AccountManagerInfoModel, AccountManagerInfoResponse>();
                cfg.CreateMap<BeneficiaryModel, BeneficiaryResponse>();
                cfg.CreateMap<BeneficiaryRoleModel, BeneficiaryRoleResponse>();
                cfg.CreateMap<StatusModel, StatusResponse>();
                cfg.CreateMap<AccountManagerModel, AccountManagerResponse>();
                cfg.CreateMap<AccountManagerRoleModel, AccountManagerRoleRequest>();

            });
            _mapper = config.CreateMapper();
            _accountManagerService = accountManagerService;
        }




        // GET: api/<AccountManager>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AccountManager>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AccountManager>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AccountManager>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountManager>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
