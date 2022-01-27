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
        //public void AddDonation(int nameId, decimal amount, DateTime dateOfCharity)
        //{
        //    var donation = new NPOManagementContext().Donations.Add(new DonationModel
        //    {
        //        DateOfCharity = dateOfCharity,
        //        Amount = amount,
        //        InvestorId = nameId
        //    });
        //}
    }
}
