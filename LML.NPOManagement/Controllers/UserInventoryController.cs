using AutoMapper;
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
    public class UserInventoryController : ControllerBase
    {
        private IMapper _mapper;
        private IUserInventoryService _userInventoryService;
        private INotificationService _notificationService;
        private IUserService _userService;

        public UserInventoryController(IUserInventoryService userInventoryService, INotificationService notificationService, IUserService userService)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountRequest, AccountModel>();
                cfg.CreateMap<AccountProgressRequest, AccountProgressModel>();
                cfg.CreateMap<AttachmentRequest, AttachmentModel>();
                cfg.CreateMap<DonationRequest, DonationModel>();
                cfg.CreateMap<InventoryTypeRequest, InventoryTypeModel>();
                cfg.CreateMap<InvestorInformationRequest, InvestorInformationModel>();
                cfg.CreateMap<NotificationRequest, NotificationModel>();
                cfg.CreateMap<RoleRequest, RoleModel>();
                cfg.CreateMap<UserInformationRequest, UserInformationModel>();
                cfg.CreateMap<UserInventoryRequest, UserInventoryModel>();
                cfg.CreateMap<UserRequest, UserModel>();
                cfg.CreateMap<AccountModel, AccountResponse>();
                cfg.CreateMap<AccountProgressModel, AccountProgressResponse>();
                cfg.CreateMap<AttachmentModel, AttachmentResponse>();
                cfg.CreateMap<DonationModel, DonationResponse>();
                cfg.CreateMap<InventoryTypeModel, InventoryTypeResponse>();
                cfg.CreateMap<InvestorInformationModel, InvestorInformationResponse>();
                cfg.CreateMap<InvestorTierTypeModel, InvestorTierTypeResponse>();
                cfg.CreateMap<NotificationModel, NotificationResponse>();
                cfg.CreateMap<NotificationTransportTypeModel, NotificationTypeResponse>();
                cfg.CreateMap<RoleModel, RoleResponse>();
                cfg.CreateMap<UserInformationModel, UserInformationResponse>();
                cfg.CreateMap<UserInventoryModel, UserInventoryResponse>();
                cfg.CreateMap<UserModel, UserResponse>();
                cfg.CreateMap<UserTypeModel, UserTypeResponse>();
                cfg.CreateMap<LoginRequest, UserModel>();
            });
            _mapper = config.CreateMapper();
            _userInventoryService = userInventoryService;
            _notificationService = notificationService;
            _userService = userService;
        }

        // GET: api/<UserInventoryController>
        [HttpGet]
        public async Task<IEnumerable<UserInventoryResponse>> Get()//???????
        {
            var inventories = await _userInventoryService.GetAllUserInventories();
            return _mapper.Map<List<UserInventoryModel>,List<UserInventoryResponse>>(inventories);
        }

        // GET: api/<UserInventoryController>
        [HttpGet("inventoryType")]
        public async Task<ActionResult<string>> GetInventoryType(string type, DateTime dateTimeStart, DateTime dateTimeFinsh)
        {
            if(type == null)
            {
                return BadRequest();
            }
            var inventoryAmount = await _userInventoryService.GetAllInventoryTypes(type, dateTimeStart, dateTimeFinsh);
            if(inventoryAmount == null)
            {
                return NotFound();
            }
            return inventoryAmount;
        }

        // GET: api/<UserInventoryController>
        [HttpGet("inventoryByTime")]
        public async Task<ActionResult<UserInventoryResponse>> GetInventoryByTime(DateTime dateTimeStart, DateTime dateTimeFinsh)
        {            
            var inventories = await _userInventoryService.GetInventoryByYear(dateTimeStart, dateTimeFinsh);
            if (inventories == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<List<UserInventoryModel>, List<UserInventoryResponse>>(inventories));
        }

        // GET: api/<UserInventoryController>
        [HttpGet("inventoryByUserTime")]
        public async Task<ActionResult<UserInventoryResponse>> GetInventoryUserByTime(int id, DateTime dateTimeStart, DateTime dateTimeFinsh)//convert datetime 2 avelacnel 2 kalonka status ev amount quantity
        {
            var inventory = await _userService.GetUserById(id);
            if(inventory == null)
            {
                return BadRequest("Not user");
            }
            var inventories = await _userInventoryService.GetInventoryUserByYear(dateTimeStart, dateTimeFinsh, id);
            if(inventories == null)
            {
                return BadRequest("Not inventory");
            }
            return Ok(_mapper.Map<List<UserInventoryModel>, List<UserInventoryResponse>>(inventories));
        }

        // GET api/<UserInventoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult< UserInventoryResponse>> Get(int id)//jnjel
        {
            var inventory = await _userInventoryService.GetUserInventoryById(id);
            if(inventory == null)
            {
                return BadRequest();
            }
            return Ok(inventory);
        }

        // GET api/<UserInventoryController>/5
        [HttpGet("userId")]
        public async Task<ActionResult<UserInventoryResponse>> GetInventoryByUserId(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return BadRequest();
            }
            var userInventories = await _userInventoryService.GetInventoryByUser(id);
            if(userInventories == null)
            {
                return BadRequest();
            }
            return Ok(userInventories);
        }

        // POST api/<UserInventoryController>
        [HttpPost]
        public async Task<ActionResult<UserInventoryResponse>> Post([FromBody] UserInventoryRequest userInventoryRequest)
        {
            var user = await _userService.GetUserById(userInventoryRequest.UserId);
            if(user == null)
            {
                return BadRequest();
            }
            var inventoryType = await _userInventoryService.GetUserInventoryTypeById(userInventoryRequest.InventoryTypeId);
            if(inventoryType == null)
            {
                return BadRequest();
            }
            var inventoryModel = _mapper.Map<UserInventoryRequest,UserInventoryModel>(userInventoryRequest);
            var inventory = await _userInventoryService.AddUserInventory(inventoryModel);
            var inventoryResponse = _mapper.Map<UserInventoryModel,UserInventoryResponse>(inventory);
            return Ok(inventoryResponse);
        }

        // POST api/<UserInventoryController>
        [HttpPost("inventoryType")]
        public async Task<ActionResult<InventoryTypeResponse>> PostInventoryType([FromBody] InventoryTypeRequest inventoryTypeRequest)
        {
            var inventoryType = _mapper.Map<InventoryTypeRequest,InventoryTypeModel>(inventoryTypeRequest);
            var newInventory = await _userInventoryService.AddInventoryType(inventoryType);
            return Ok(_mapper.Map<InventoryTypeModel,InventoryTypeResponse>(newInventory));
        }

        // PUT api/<UserInventoryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserInventoryResponse>> Put(int id, [FromBody] UserInventoryRequest userInventoryRequest)
        {
            var inventory = await _userInventoryService.GetUserInventoryById(id);
            if( inventory == null)
            {
                return BadRequest();
            }
            var user = await _userService.GetUserById(userInventoryRequest.UserId);
            if (user == null)
            {
                return BadRequest();
            }
            var inventoryType = await _userInventoryService.GetUserInventoryTypeById(userInventoryRequest.InventoryTypeId);
            if (inventoryType == null)
            {
                return BadRequest();
            }
            var inventoryModel = _mapper.Map<UserInventoryRequest, UserInventoryModel>(userInventoryRequest);
            var newInventory = await _userInventoryService.ModifyUserInventory(inventoryModel, id);
            return Ok(newInventory);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var inventory = await _userInventoryService.GetUserInventoryById(id);
            if (inventory == null)
            {
                return BadRequest();
            }
            var newInventory = await _userInventoryService.DeleteInventory(id);  
            return Ok(newInventory);          
        }
    }
}
