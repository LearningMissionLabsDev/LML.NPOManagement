﻿
using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface IUserGroupRepository
    {
        DbSet<UsersGroup> UsersGroups { get; set; }
    }
}
