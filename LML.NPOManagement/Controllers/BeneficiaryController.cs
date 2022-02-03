using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        // GET: api/<BeneficiaryController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BeneficiaryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
