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
    public class InvestorInformationController : ControllerBase
    {
        private IMapper _mapper;
        private IInvestorInformationService _investorInformationService;
        public InvestorInformationController(IInvestorInformationService investorInformationService)
        {
            _investorInformationService = investorInformationService;
        }
        // GET: api/<InvestorInformationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<InvestorInformationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InvestorInformationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<InvestorInformationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InvestorInformationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
