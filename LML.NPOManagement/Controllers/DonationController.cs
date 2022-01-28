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
    public class DonationController : ControllerBase
    {
        private IMapper _mapper;
        private IDonationService _donationService;
        public DonationController(IDonationService donationService)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DonationModel, DonationResponse>();
                cfg.CreateMap<InvestorModel, InvestorResponse>();
            });

            _mapper = config.CreateMapper();
            _donationService = donationService;
        }
    
        // GET: api/<DonationController>
        [HttpGet]
        public IEnumerable<DonationResponse> Get()
        {
            var donations = _donationService.GetAllDonations().ToList();
            return _mapper.Map<List<DonationModel>,List<DonationResponse>>(donations);
        }

        // GET api/<DonationController>/5
        [HttpGet("{id}")]
        public DonationResponse Get(int id)
        {
            var donation = _donationService.GetDonationById(id);
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
        public void Delete(int id)
        {
        }
    }
}
