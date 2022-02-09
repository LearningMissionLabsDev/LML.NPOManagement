using AutoMapper;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Bll.Independencies;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;
using LML.NPOManagement.Request;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestorController : ControllerBase
    {
        private IMapper _mapper;
        private IInvestorService _investorService;
        public InvestorController(IInvestorService investorService)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DonationModel, DonationResponse>();
                cfg.CreateMap<InvestorModel, InvestorResponse>();
                cfg.CreateMap<DonationRequest, DonationModel>();
                cfg.CreateMap<InvestorRequest, InvestorModel>();
                cfg.CreateMap<InvestorModel, InvestorRequest>();

            });

            _mapper = config.CreateMapper();
            _investorService = investorService;
        }
        // GET: api/<InvestorController>
        [HttpGet]
        public IEnumerable<InvestorResponse> Get()
        {
            var investors = _investorService.GetAllInvestors().ToList();
            return _mapper.Map<List<InvestorModel>, List<InvestorResponse>>(investors);
        }

        // GET api/<InvestorController>/5
        [HttpGet("{id}")]
        public InvestorResponse Get(int id)
        {
            var investor = _investorService.GetInvestorById(id);
            return _mapper.Map<InvestorModel, InvestorResponse>(investor);
        }

        // POST api/<InvestorController>
        [HttpPost]
        public async Task<InvestorResponse> Post([FromBody] InvestorRequest investorRequest)
        {
            var addInvestor = _mapper.Map<InvestorRequest, InvestorModel>(investorRequest);
            var id = _investorService.AddInvestor(addInvestor);
            var investorModel = _investorService.GetInvestorById(id);
            return _mapper.Map<InvestorModel, InvestorResponse>(investorModel);
        }

        // PUT api/<InvestorController>/5
        [HttpPut("{id}")]
        public async Task<InvestorResponse> Put(int id, [FromBody] InvestorRequest investorRequest)
        {
            var modifyInvestor = _mapper.Map<InvestorRequest, InvestorModel>(investorRequest);
            var investorId = _investorService.ModifyInvestor(modifyInvestor, id);
            var investorModel = _investorService.GetInvestorById(investorId);
            return _mapper.Map<InvestorModel, InvestorResponse>(investorModel);
        }

        // DELETE api/<InvestorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var investorToDelete = _investorService.GetInvestorById(id);
            if (investorToDelete == null)
            {
                return NotFound();
            }
            _investorService.DeleteInvestor(id);

            return Ok();
        }
    }
}
