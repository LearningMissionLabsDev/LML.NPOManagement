namespace LML.NPOManagement.Bll.Model
{
    public class InvestorModel
    {
        public InvestorModel()
        {
            Donations = new HashSet<DonationModel>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int PhoneNumber { get; set; }
        public string Email { get; set; } = null!;

        public virtual ICollection<DonationModel> Donations { get; set; }
    }
}
