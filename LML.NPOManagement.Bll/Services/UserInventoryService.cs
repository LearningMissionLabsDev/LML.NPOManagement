using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Services
{
    public class UserInventoryService : IUserInventoryService
    {
        private IMapper _mapper;
        public UserInventoryService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserInventory, UserInventoryModel>();
                cfg.CreateMap<UserInventoryModel, UserInventory>();
            });
            _mapper = config.CreateMapper();
        }

        public int AddUserInventory(UserInventoryModel userInventoryModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserInventory(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserInventoryModel> GetAllUserInventories()
        {
            throw new NotImplementedException();
        }

        public UserInventoryModel GetUserInventoryById(int id)
        {
            throw new NotImplementedException();
        }

        public int ModifyUserInventory(UserInventoryModel userInventoryModel, int id)
        {
            throw new NotImplementedException();
        }
    }
}
