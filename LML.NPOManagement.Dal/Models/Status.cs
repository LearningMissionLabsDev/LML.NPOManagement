﻿using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class Status
    {
        public Status()
        {
            AccountManagerInfos = new HashSet<AccountManagerInfo>();
            Beneficiaries = new HashSet<Beneficiary>();
        }

        public int Id { get; set; }
        public string StatusTayp { get; set; } = null!;

        public virtual ICollection<AccountManagerInfo> AccountManagerInfos { get; set; }
        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }
    }
}
