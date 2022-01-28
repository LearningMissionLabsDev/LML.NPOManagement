using AutoMapper;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace LML.NPOManagement.Bll.Services
{
    public class DonationService
    {
        private IMapper _mapper;
        public DonationService()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Donation, DonationModel>();
                cfg.CreateMap<Investor, InvestorModel>();
                cfg.CreateMap<DonationModel, Donation>();
                cfg.CreateMap<InvestorModel, Investor>();
            });
             _mapper = config.CreateMapper();
        }

        public IEnumerable<DonationModel> GetAllDonations()
        {
            using(var dbContext = new NPOManagementContext())
            {
                var donations = dbContext.Donations.ToList();

                foreach (var donation in donations)
                {
                    var donationModel = _mapper.Map<Donation, DonationModel>(donation);
                    yield return donationModel;
                }
            }           
        }

        public DonationModel GetDonationById(int id)
        {
            using(var dbContext = new NPOManagementContext())
            {
                var donation = dbContext.Donations.Include(d=>d.Investor).Where(donation => donation.Id == id).FirstOrDefault();
                if (donation != null)
                {
                    var donationModel = _mapper.Map<Donation, DonationModel>(donation);
                    return donationModel;
                }
                return null;
            }
        }
        public void AddDonation(DonationModel donationModel)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var donation = _mapper.Map<DonationModel, Donation>(donationModel);
                dbContext.Donations.Add(donation);
                dbContext.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            using (var dbcontext = new NPOManagementContext())
            {
                var donation = dbcontext.Donations.FirstOrDefault(d => d.Id == id);
                if (donation != null)
                {
                    dbcontext.Donations.Remove(donation);
                    dbcontext.SaveChanges();

                }
                //Donation donation = new Donation() { Id = id };
                //dbcontext.Donations.Attach(donation);
                //dbcontext.Donations.Remove(donation);
                //dbcontext.SaveChanges();
            }
            
                
            
        }
       
        
        
        
        
    

    }
}

//public HttpResponseMessage Delete(int id)
//{

//    }
//}