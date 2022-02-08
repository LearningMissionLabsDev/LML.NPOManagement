
namespace LML.NPOManagement.Bll.Model
{
    public class AccountManagerInfoModel
    {
        public AccountManagerInfoModel()
        {
            AccountManagerInventories = new HashSet<AccountManagerInventoryModel>();
        }

        public int Id { get; set; }
        public int AccountManagerInfoRoleId { get; set; }
        public ActivityStatus ActivityStatusType { get; set; }
        public int AccountManagerCategoryId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public GenderModel Gender { get; set; } 
        public string Information { get; set; }

        public virtual AccountModel AccountManagerCategory { get; set; } = null!;
        public virtual AccountManagerRoleModel AccountManagerCategoryNavigation { get; set; } = null!;
        public virtual StatusModel Status { get; set; } = null!;
        public virtual ICollection<AccountManagerInventoryModel> AccountManagerInventories { get; set; }
    }
}
