using AutoMapper;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Bll.Services;
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
        public InvestorController()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DonationModel, DonationResponse>();
                cfg.CreateMap<InvestorModel, InvestorResponse>();
            });
            _mapper = config.CreateMapper();
        }
        // GET: api/<InvestorController>
        [HttpGet]
        public IEnumerable<InvestorResponse> Get()
        {
            var investors = new InvestorService().GetAllInvestors().ToList();
            return _mapper.Map<List<InvestorModel>, List<InvestorResponse>>(investors);
        }

        // GET api/<InvestorController>/5
        [HttpGet("{id}")]
        public InvestorResponse Get(int id)
        {
            var investor = new InvestorService().GetInvestorById(id);
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
        public void Delete(int id)
        {
        }
    }
}
