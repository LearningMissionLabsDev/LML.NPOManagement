using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface IUserRepository
    {
        DbSet<User> Users { get; set; }
        DbSet<UserInformation> UserInformations { get; set; }
        DbSet<UserType> UserTypes { get; set; }
        DbSet<Role> Roles { get; set; }
    }
}
