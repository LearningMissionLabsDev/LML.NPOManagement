using LML.NPOManagement.Common.Model;

namespace LML.NPOManagement.Response
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public int? StatusId { get; set; }
        public int CreatorId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
