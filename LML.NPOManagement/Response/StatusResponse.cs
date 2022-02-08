using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Response
{
    public class StatusResponse
    {
        public ActivityStatus ActivityStatusType { get; set; } 

        public virtual ICollection<AccountManagerInfoResponse> AccountManagerInfosRes { get; set; }
        public virtual ICollection<BeneficiaryResponse> BeneficiariesRes { get; set; }
    }
}
