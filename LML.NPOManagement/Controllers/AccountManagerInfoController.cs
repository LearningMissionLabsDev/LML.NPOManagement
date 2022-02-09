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
    public class AccountManagerInfoController : ControllerBase
    {
        private IMapper _mapper;
        private IAccountManagerInfoService  _accountManagerInfoService;
        public AccountManagerInfoController(IAccountManagerInfoService accountManagerInfoService)
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
            _accountManagerInfoService = accountManagerInfoService;
        }
        // GET: api/<AccountManagerInfoController>
        [HttpGet]
        public IEnumerable<AccountManagerInfoResponse> Get()
        {
            var accountManagerInfo = _accountManagerInfoService.GetAllAccountManagerInfos().ToList();

            return _mapper.Map<List<AccountManagerInfoModel>, List<AccountManagerInfoResponse>>(accountManagerInfo);
        }

        // GET api/<AccountManagerInfoController>/5
        [HttpGet("{id}")]
        public AccountManagerInfoResponse Get(int id)
        {
            var accountManagerInfo = _accountManagerInfoService.GetAccountManagerInfoById(id);

            return _mapper.Map<AccountManagerInfoModel, AccountManagerInfoResponse>(accountManagerInfo);
        }

        // POST api/<AccountManagerInfoController>
        [HttpPost]
        public async Task<AccountManagerInfoResponse> Post([FromBody] AccountManagerInfoRequest accountManagerInfoRequest)
        {
            var addAccountManagerInfo = _mapper.Map<AccountManagerInfoRequest, AccountManagerInfoModel>(accountManagerInfoRequest);
            var id = _accountManagerInfoService.AddAccountManagerInfo(addAccountManagerInfo);
            var accountManagerInfoModel = _accountManagerInfoService.GetAccountManagerInfoById(id);

            return _mapper.Map<AccountManagerInfoModel, AccountManagerInfoResponse>(accountManagerInfoModel);
        }

        // PUT api/<AccountManagerInfoController>/5
        [HttpPut("{id}")]
        public async Task<AccountManagerInfoResponse> Put(int id, [FromBody] AccountManagerInfoRequest accountManagerInfoRequest)
        {
            var modifyAccountManagerInfo = _mapper.Map<AccountManagerInfoRequest, AccountManagerInfoModel>(accountManagerInfoRequest);
            var accountManagerInfoId = _accountManagerInfoService.ModifyAccountManagerInfo(modifyAccountManagerInfo, id);
            var accountManagerInfoModel = _accountManagerInfoService.GetAccountManagerInfoById(accountManagerInfoId);

            return _mapper.Map<AccountManagerInfoModel, AccountManagerInfoResponse>(accountManagerInfoModel);
        }

        // DELETE api/<AccountManagerInfoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var accountManagerInfoToDelete = _accountManagerInfoService.GetAccountManagerInfoById(id);
            if (accountManagerInfoToDelete == null)
            {
                return NotFound();
            }

            _accountManagerInfoService.DeleteAccountManagerInfo(id);

            return Ok();
        }
    }
}
