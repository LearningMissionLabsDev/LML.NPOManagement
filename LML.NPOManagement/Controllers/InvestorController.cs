using AutoMapper;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Bll.Independencies;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;

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
        public void Post([FromBody] string value)
        {
           
        }

        // PUT api/<InvestorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InvestorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var investor = new InvestorService().Delete;
            return Ok();
        }
    }
}
