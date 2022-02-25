using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class AccountProgressResponse
    {
        /// <summary>
        /// From Progress in account
        /// </summary>
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; } 

        public virtual AccountResponse Account { get; set; } 
    }
}
