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

        public AccountController(IAccountService accountService)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountManagerInfoRequest, AccountManagerInfoModel>();
                cfg.CreateMap<BeneficiaryRequest, BeneficiaryModel>();
                cfg.CreateMap<BeneficiaryRoleRequest, BeneficiaryRoleModel>();
                cfg.CreateMap<StatusRequest, StatusModel>();
                cfg.CreateMap<AccountRequest, AccountModel>();
                cfg.CreateMap<AccountManagerRoleRequest, AccountManagerRoleModel>();
                cfg.CreateMap<AccountManagerInventoryRequest,  AccountManagerInventoryModel > ();
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
            _accountService = accountService;
        }
        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<AccountManagerInfoResponse> Get()
        {
            var accounts = _accountService.GetAllAccounts().ToList();
            return _mapper.Map<List<AccountModel>, List<AccountManagerInfoResponse>>(accounts);
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public AccountManagerInfoResponse Get(int id)
        {

            var account = _accountService.GetAccountById(id);
            return _mapper.Map<AccountModel, AccountManagerInfoResponse>(account);
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task<AccountManagerInfoResponse> Post([FromBody] AccountManagerInfoRequest accountManagerInfoRequest)
        {
            var addAccount = _mapper.Map<AccountManagerInfoRequest, AccountModel>(accountManagerInfoRequest);
            var id = _accountService.AddAccountManagerInfo(addAccount);
            var accountModel = _accountService.GetAccountById(id);
            return _mapper.Map<AccountModel, AccountManagerInfoResponse>(accountModel);
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public async Task<AccountManagerInfoResponse> Put(int id, [FromBody] AccountManagerInfoRequest accountManagerInfoRequest)
        {
            var modifyAccount = _mapper.Map<AccountManagerInfoRequest, AccountModel>(accountManagerInfoRequest);
            var accountId = _accountService.ModifyAccount(modifyAccount, id);
            var accountModel = _accountService.GetAccountById(accountId);
            return _mapper.Map<AccountModel, AccountManagerInfoResponse>(accountModel);
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var accountForDelete = _accountService.GetAccountById(id);
            if (accountForDelete == null)
            {
                return NotFound();
            }
           _accountService.DeleteAccount(id);

            return Ok();

        }
    }
}
