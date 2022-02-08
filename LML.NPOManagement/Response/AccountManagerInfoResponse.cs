using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Response
{
    public class AccountManagerInfoResponse
    {      
        public int AccountManagerInfoRoleId { get; set; }
        public ActivityStatus ActivityStatusType { get; set; }
        public int AccountManagerCategoryId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }       
        public GenderModel Gender { get; set; }
      

        public virtual AccountResponse AccountManagerCategoryRes { get; set; } = null!;
        public virtual AccountManagerRoleResponse AccountManagerCategoryNavigationRes { get; set; } = null!;
        public virtual StatusResponse StatusRes { get; set; } = null!;
        public virtual ICollection<AccountManagerInventoryResponse> AccountManagerInventoriesRes { get; set; }
    }
}
