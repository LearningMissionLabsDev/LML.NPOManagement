using LML.NPOManagement.Dal.Models;
using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string AccountName { get; set; } 
        public string AccountDescription { get; set; }

    }
}
