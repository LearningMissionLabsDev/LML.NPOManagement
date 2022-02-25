using LML.NPOManagement.Dal.Models;
using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class AccountResponse
    {
        public AccountResponse()
        {
            AccountProgresses = new HashSet<AccountProgressResponse>();
            Users = new HashSet<UserResponse>();
        }

        public int Id { get; set; }
        public string AccountName { get; set; } = null!;
        public string AccountDescription { get; set; }

        public virtual ICollection<AccountProgressResponse> AccountProgresses { get; set; }

        public virtual ICollection<UserResponse> Users { get; set; }
    }
}
