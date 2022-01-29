using AutoMapper;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Bll.Independencies;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;
using LML.NPOManagement.Request;

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
                cfg.CreateMap<DonationRequest, DonationModel>();
                cfg.CreateMap<InvestorRequest, InvestorModel>();
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
        public async Task<DonationResponse> Post([FromBody] DonationRequest donationRequest)
        {
            var addDonation = _mapper.Map<DonationRequest, DonationModel>(donationRequest);
            var id = _donationService.AddDonation(addDonation);
            var donationModel = _donationService.GetDonationById(id);
            return _mapper.Map<DonationModel,DonationResponse>(donationModel);
        }

        // PUT api/<DonationController>/5
        [HttpPut("{id}")]
        public async Task<DonationResponse> Put(int id, [FromBody] DonationRequest donationRequest)
        {
            var modifyDonation = _mapper.Map<DonationRequest,DonationModel>(donationRequest);
            var donationId = _donationService.ModifyDonation(modifyDonation, id);
            var donationModel = _donationService.GetDonationById(donationId);
            return _mapper.Map<DonationModel, DonationResponse>(donationModel);
        }

        // DELETE api/<DonationController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }
            var donationToDelete = _donationService.GetDonationById(id);
            if(donationToDelete == null)
            {
                return NotFound();           
            }
            _donationService.DeleteDonation(id);

            return Ok();
            
        }
    }
}
