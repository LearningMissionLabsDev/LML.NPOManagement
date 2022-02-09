﻿using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Request;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryRoleController : ControllerBase
    {

        private IMapper _mapper;
        private IBeneficiaryRoleService _roleService;

        public BeneficiaryRoleController(IBeneficiaryRoleService roleService)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountManagerInfoRequest, AccountManagerInfoModel>();
                cfg.CreateMap<BeneficiaryRequest, BeneficiaryModel>();
                cfg.CreateMap<BeneficiaryRoleRequest, BeneficiaryRoleModel>();
                cfg.CreateMap<StatusRequest, StatusModel>();
                cfg.CreateMap<AccountRequest, AccountModel>();
                cfg.CreateMap<AccountManagerRoleRequest, AccountManagerRoleModel>();
                cfg.CreateMap<AccountManagerInventoryRequest, AccountManagerInventoryModel>();
                cfg.CreateMap<BeneficiaryInventoryRequest, BeneficiaryInventoryModel>();
                cfg.CreateMap<InventoryTypeRequest, InventoryTypeModel>();
                cfg.CreateMap<AccountManagerInfoModel, AccountManagerInfoResponse>();
                cfg.CreateMap<BeneficiaryModel, BeneficiaryResponse>();
                cfg.CreateMap<BeneficiaryRoleModel, BeneficiaryRoleResponse>();
                cfg.CreateMap<StatusModel, StatusResponse>();
                cfg.CreateMap<AccountModel, AccountResponse>();
                cfg.CreateMap<AccountManagerRoleModel, AccountManagerRoleRequest>();
                cfg.CreateMap<AccountManagerInventoryModel, AccountManagerInventoryResponse>();
                cfg.CreateMap<BeneficiaryInventoryModel, BeneficiaryInventoryResponse>();
                cfg.CreateMap<InventoryTypeModel, InventoryTypeResponse>();

            });
            _mapper = config.CreateMapper();
            _roleService = roleService;
        }


        // GET: api/<RoleController>
        [HttpGet]
        public IEnumerable<BeneficiaryRoleResponse> Get()
        {
            var beneficiaryRoles = _roleService.GetAllBeneficiaryRoles().ToList();
            return _mapper.Map<List<BeneficiaryRoleModel>, List<BeneficiaryRoleResponse>>(beneficiaryRoles);
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public BeneficiaryRoleResponse Get(int id)
        {
            var beneficiaryRole = _roleService.GetBeneficiaryRoleById(id);
            return _mapper.Map<BeneficiaryRoleModel, BeneficiaryRoleResponse>(beneficiaryRole);
        }

       
    }
}
