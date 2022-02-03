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
    public class StatusServices : IStatus
    {
        private IMapper _mapper;
        public StatusServices()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountManagerInfo, AccountManagerInfoModel>();
                cfg.CreateMap<Beneficiary, BeneficiaryModel>();
                cfg.CreateMap<Role, RoleModel>();
                cfg.CreateMap<Status, StatusModel>();
                cfg.CreateMap<AccountManager, AccountManagerModel>();
                cfg.CreateMap<AccountManagerInfoModel, AccountManagerInfo>();
                cfg.CreateMap<BeneficiaryModel, Beneficiary>();
                cfg.CreateMap<RoleModel, Role>();
                cfg.CreateMap<StatusModel, Status>();
                cfg.CreateMap<AccountManagerModel, AccountManager>();

            });
            _mapper = config.CreateMapper();
        }

        public IEnumerable<StatusModel> GetAllStatus()
        {
            throw new NotImplementedException();
        }

        public StatusModel GetStatusById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
