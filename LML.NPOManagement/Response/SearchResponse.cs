using System;
using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Response
{
    public class SearchResponse
    {
        public int Id { get; set; }
        public SearchType Type { get; set; }
        public String Name { get; set; }
    }
}

