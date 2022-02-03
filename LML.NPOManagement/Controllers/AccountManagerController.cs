using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountManagerController : ControllerBase
    {
        // GET: api/<AccountManager>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AccountManager>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AccountManager>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AccountManager>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountManager>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
