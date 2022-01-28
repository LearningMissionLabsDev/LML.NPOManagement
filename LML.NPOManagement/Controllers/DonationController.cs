
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
    public class DonationController : ControllerBase
    {
        private IMapper _mapper;
        public DonationController()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DonationModel, DonationResponse>();
                cfg.CreateMap<InvestorModel, InvestorResponse>();
            });
            _mapper = config.CreateMapper();
        }
    
        // GET: api/<DonationController>
        [HttpGet]
        public IEnumerable<DonationResponse> Get()
        {
            var donations = new DonationService().GetAllDonations().ToList();
            return _mapper.Map<List<DonationModel>,List<DonationResponse>>(donations);
        }

        // GET api/<DonationController>/5
        [HttpGet("{id}")]
        public DonationResponse Get(int id)
        {
            var donation = new DonationService().GetDonationById(id);
            return _mapper.Map<DonationModel, DonationResponse>(donation);
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
        public IActionResult Delete(int id)
        {
            if(id <=0)
            {
                return BadRequest();
            }
            var donationService = new DonationService();
            var donationToDelete = donationService.GetDonationById(id);
            if(donationToDelete == null)
            {
                return NotFound();           
            }
            donationService.Delete(id);

            return Ok();
            
        }
    }
}
