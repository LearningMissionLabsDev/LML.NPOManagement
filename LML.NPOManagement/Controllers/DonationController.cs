
using LML.NPOManagement.Bll.Models;
using LML.NPOManagement.Bll.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        // GET: api/<DonationController>
        [HttpGet]
        public IEnumerable<DonationResponse> Get()
        {            
            return new DonationService().GetAllDonations();
        }

        // GET api/<DonationController>/5
        [HttpGet("{id}")]
        public DonationResponse? Get(int id)
        {
            return new DonationService().GetDonationById(id);
        }

        // POST api/<DonationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DonationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DonationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
