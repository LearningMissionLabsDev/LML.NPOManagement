using LML.NPOManagement.Common.Model;

namespace LML.NPOManagement.Response
{
    public class SearchResponse
    {
        public int Id { get; set; }
        public SearchTypeEnum Type { get; set; }
        public String Name { get; set; }
    }
}
