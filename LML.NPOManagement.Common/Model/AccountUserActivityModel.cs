namespace LML.NPOManagement.Common.Model
{
    public class AccountUserActivityModel
    {
        public int Id { get; set; }

        public int Account2UserId { get; set; }

        public DateTime DateCreated { get; set; }

        public string ActivityInfo { get; set; } = null!;

        public virtual Account2UserModel Account2UserModel { get; set; } = null!;
    }
}
