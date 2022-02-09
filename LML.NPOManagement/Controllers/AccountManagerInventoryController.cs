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
    public class AccountManagerInventoryController : ControllerBase
    {
        private IMapper _mapper;
        private IAccountManagerInventoryService _accountManagerInventoryService;

        public AccountManagerInventoryController(IAccountManagerInventoryService accountManagerInventoryService)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountManagerInfoRequest, AccountManagerInfoModel>();
                cfg.CreateMap<BeneficiaryRequest, BeneficiaryModel>();
                cfg.CreateMap<BeneficiaryRoleRequest, BeneficiaryRoleModel>();
                cfg.CreateMap<StatusRequest, StatusModel>();
                cfg.CreateMap<AccountRequest, AccountModel>();
                cfg.CreateMap<AccountManagerRoleRequest, AccountManagerRoleModel>();
                cfg.CreateMap<AccountManagerInventoryRequest, AccountManagerInventoryModel>();
                cfg.CreateMap<BeneficiaryInventoryRequest, BeneficiaryInventoryModel>();
                cfg.CreateMap<InventoryTypeRequest, InventoryTypeModel>();
                cfg.CreateMap<AccountManagerInfoModel, AccountManagerInfoResponse>();
                cfg.CreateMap<BeneficiaryModel, BeneficiaryResponse>();
                cfg.CreateMap<BeneficiaryRoleModel, BeneficiaryRoleResponse>();
                cfg.CreateMap<StatusModel, StatusResponse>();
                cfg.CreateMap<AccountModel, AccountResponse>();
                cfg.CreateMap<AccountManagerRoleModel, AccountManagerRoleRequest>();
                cfg.CreateMap<AccountManagerInventoryModel, AccountManagerInventoryResponse>();
                cfg.CreateMap<BeneficiaryInventoryModel, BeneficiaryInventoryResponse>();
                cfg.CreateMap<InventoryTypeModel, InventoryTypeResponse>();

            });
            _mapper = config.CreateMapper();
            _accountManagerInventoryService = accountManagerInventoryService;
        }
        // GET: api/<AccountManagerInventoryController>
        [HttpGet]
        public IEnumerable<AccountManagerInventoryResponse> Get()
        {
            var accountManagerInventory = _accountManagerInventoryService.GetAllAccountManagerInventories().ToList();
            return _mapper.Map<List<AccountManagerInventoryModel>,List<AccountManagerInventoryResponse>>(accountManagerInventory);
        }

        // GET api/<AccountManagerInventoryController>/5
        [HttpGet("{id}")]
        public AccountManagerInventoryResponse Get(int id)
        {
            var accountManagerInventory = _accountManagerInventoryService.GetAccountManagerInventoryById(id);
            return _mapper.Map<AccountManagerInventoryModel,AccountManagerInventoryResponse>(accountManagerInventory);
        }

        // POST api/<AccountManagerInventoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AccountManagerInventoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountManagerInventoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
