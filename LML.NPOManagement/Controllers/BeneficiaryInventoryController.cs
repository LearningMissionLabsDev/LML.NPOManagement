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
    public class BeneficiaryInventoryController : ControllerBase
    {
        private IMapper _mapper;
        private IBeneficiaryInventoryService _beneficiaryInventoryService;

        public BeneficiaryInventoryController(IBeneficiaryInventoryService beneficiaryInventoryService)
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
            _beneficiaryInventoryService = beneficiaryInventoryService;
        }
        // GET: api/<BeneficiaryInventoryController>
        [HttpGet]
        public IEnumerable<BeneficiaryInventoryResponse> Get()
        {
            var beneficiaryInventory = _beneficiaryInventoryService.GetAllBeneficiaryInventories().ToList();
            return _mapper.Map<List<BeneficiaryInventoryModel>,List<BeneficiaryInventoryResponse>>(beneficiaryInventory);
        }

        // GET api/<BeneficiaryInventoryController>/5
        [HttpGet("{id}")]
        public BeneficiaryInventoryResponse Get(int id)
        {
            var beneficiaryInventory = _beneficiaryInventoryService.GetBeneficiaryInventoryById(id);
            return _mapper.Map<BeneficiaryInventoryModel,BeneficiaryInventoryResponse>(beneficiaryInventory);
        }

        // POST api/<BeneficiaryInventoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BeneficiaryInventoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BeneficiaryInventoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
