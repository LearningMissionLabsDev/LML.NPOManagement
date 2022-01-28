using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Independencies
{
    public interface IDonationService
    {
        public IEnumerable<DonationModel> GetAllDonations();
        public DonationModel GetDonationById(int id);
        public int AddDonation(DonationModel donationModel);
        public void ModifyDonation(DonationModel donationModel);    
        public void DeleteDonation(int id);
    }
}
