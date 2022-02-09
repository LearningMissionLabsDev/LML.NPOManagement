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
    public class BeneficiaryController : ControllerBase
    {
        private IMapper _mapper;
        private IBeneficiaryService _beneficiaryService;

        public BeneficiaryController(IBeneficiaryService beneficiaryService)
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
            _beneficiaryService = beneficiaryService;
        }


        // GET: api/<BeneficiaryController>
        [HttpGet]
        public IEnumerable<BeneficiaryResponse> Get()
        {
            var beneficiary = _beneficiaryService.GetAllBeneficiaries().ToList();
            return _mapper.Map<List<BeneficiaryModel>,List<BeneficiaryResponse>>(beneficiary);
        }

        // GET api/<BeneficiaryController>/5
        [HttpGet("{id}")]
        public BeneficiaryResponse Get(int id)
        {
            var beneficiary = _beneficiaryService.GetBeneficiaryById(id);
            return _mapper.Map<BeneficiaryModel,BeneficiaryResponse>(beneficiary);
        }

        // POST api/<BeneficiaryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BeneficiaryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BeneficiaryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
