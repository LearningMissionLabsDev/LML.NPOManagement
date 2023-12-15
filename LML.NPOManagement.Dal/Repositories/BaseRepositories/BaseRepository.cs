using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories.Interfaces;

namespace LML.NPOManagement.Dal.Repositories.BaseRepositories
{
    public class BaseRepository : NPOManagementContext,IBaseRepository, IAccountRepository, IInvestorRepository, INotificationRepository, IUserRepository, IUserInventoryRepository, IUserGroupRepository
    {
        void IAccountRepository.SaveChanges()
        {
            base.SaveChanges();
        }

        void IInvestorRepository.SaveChanges()
        {
            base.SaveChanges();
        }

        void INotificationRepository.SaveChanges()
        {
            base.SaveChanges();
        }

        void IUserRepository.SaveChanges()
        {
            base.SaveChanges();
        }

        void IUserInventoryRepository.SaveChanges()
        {
            base.SaveChanges();
        }

        void IUserGroupRepository.SaveChanges()
        {
            base.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
