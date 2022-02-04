
namespace LML.NPOManagement.Dal.Models
{
    public partial class Investor
    {
        public Investor()
        {
            Donations = new HashSet<Donation>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int PhoneNumber { get; set; }
        public string Email { get; set; } = null!;

        public virtual ICollection<Donation> Donations { get; set; }
    }
}
