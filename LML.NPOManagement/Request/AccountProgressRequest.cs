using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Request
{
    public class AccountProgressRequest
    {
        /// <summary>
        /// From Progress in account
        /// </summary>
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; } 

    }
}
