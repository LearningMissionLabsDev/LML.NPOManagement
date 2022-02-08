using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class BeneficiaryModel
    {
        public int Id { get; set; }
        public int BeneficiaryRoleId { get; set; }
        public int StatusId { get; set; }
        public int BeneficiaryCategoryId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? BeneficiaryInfo { get; set; }

        public virtual Account BeneficiaryCategory { get; set; } = null!;
        public virtual BeneficiaryRole BeneficiaryRole { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
    }
}
