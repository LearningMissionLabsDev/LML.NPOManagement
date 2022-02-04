﻿namespace LML.NPOManagement.Response
{
    public class AccountManagerResponse
    {
        public string AccountManagerCategory { get; set; } = null!;

        public virtual ICollection<AccountManagerInfoResponse> AccountManagerInfosRes { get; set; }
    }
}
