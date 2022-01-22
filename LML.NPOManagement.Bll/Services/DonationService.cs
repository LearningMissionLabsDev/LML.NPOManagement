using LML.NPOManagement.Bll.Models;
using LML.NPOManagement.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Services
{
    public class DonationService
    {
        public IEnumerable<DonationResponse> GetAllDonations()
        {
            var donations = new NPOManagementContext().Donations.ToList();
            foreach (var donation in donations)
            {
                yield return new DonationResponse(donation);
            }
        }

        public DonationResponse? GetDonationById(int id)
        {
            var donationEntity = new NPOManagementContext().Donations.Where(donation => donation.Id == id).FirstOrDefault();
            if (donationEntity != null)
            {
                return new DonationResponse(donationEntity);
            }
            return null;
        }
    }
}
