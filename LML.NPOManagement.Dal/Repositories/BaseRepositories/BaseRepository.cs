using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Dal.Repositories.BaseRepositories
{
    public class BaseRepository : NPOManagementContext, IBaseRepository, IAccountRepository, IInvestorRepository, INotificationRepository, IUserRepository, IUserInventoryRepository, IUserGroupRepository
    {
        void IBaseRepository.SaveChanges()
        {
            base.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
