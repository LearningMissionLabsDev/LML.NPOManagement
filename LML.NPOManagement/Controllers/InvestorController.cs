using LML.NPOManagement.Bll.Models;
using LML.NPOManagement.Bll.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestorController : ControllerBase
    {
        // GET: api/<InvestorController>
        [HttpGet]
        public IEnumerable<InvestorResponse> Get()
        {
            return new InvestorService().GetAllInvestors();
        }

        // GET api/<InvestorController>/5
        [HttpGet("{id}")]
        public InvestorResponse? Get(int id)
        {
            return new InvestorService().GetInvestorById(id);
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
