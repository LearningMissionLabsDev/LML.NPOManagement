using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Request;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountRequest, AccountModel>();
                cfg.CreateMap<AccountProgressRequest, AccountProgressModel>();
                cfg.CreateMap<AttachmentRequest, AttachmentModel>();
                cfg.CreateMap<DailyScheduleRequest, DailyScheduleModel>();
                cfg.CreateMap<DonationRequest, DonationModel>();
                cfg.CreateMap<InventoryTypeRequest, InventoryTypeModel>();
                cfg.CreateMap<InvestorInformationRequest, InvestorInformationModel>();
                cfg.CreateMap<InvestorTierTypeRequest, InvestorTierTypeModel>();
                cfg.CreateMap<MeetingScheduleRequest, MeetingScheduleModel>();
                cfg.CreateMap<NotificationRequest, NotificationModel>();
                cfg.CreateMap<NotificationTypeRequest, NotificationTypeModel>();
                cfg.CreateMap<RoleRequest, RoleModel>();
                cfg.CreateMap<TemplateRequest, TemplateModel>();
                cfg.CreateMap<TemplateTypeRequest, TemplateTypeModel>();
                cfg.CreateMap<UserInformationRequest, UserInformationModel>();
                cfg.CreateMap<UserInventoryRequest, UserInventoryModel>();
                cfg.CreateMap<UserRequest, UserModel>();
                cfg.CreateMap<UserTypeRequest, UserTypeModel>();
                cfg.CreateMap<WeeklyScheduleRequest, WeeklyScheduleModel>();
                cfg.CreateMap<AccountModel, AccountResponse>();
                cfg.CreateMap<AccountProgressModel, AccountProgressResponse>();
                cfg.CreateMap<AttachmentModel, AttachmentResponse>();
                cfg.CreateMap<DailyScheduleModel, DailyScheduleResponse>();
                cfg.CreateMap<DonationModel, DonationResponse>();
                cfg.CreateMap<InventoryTypeModel, InventoryTypeResponse>();
                cfg.CreateMap<InvestorInformationModel, InvestorInformationResponse>();
                cfg.CreateMap<InvestorTierTypeModel, InvestorTierTypeResponse>();
                cfg.CreateMap<MeetingScheduleModel, MeetingScheduleResponse>();
                cfg.CreateMap<NotificationModel, NotificationResponse>();
                cfg.CreateMap<NotificationTypeModel, NotificationTypeResponse>();
                cfg.CreateMap<RoleModel, RoleResponse>();
                cfg.CreateMap<TemplateModel, TemplateResponse>();
                cfg.CreateMap<TemplateTypeModel, TemplateTypeResponse>();
                cfg.CreateMap<UserInformationModel, UserInformationResponse>();
                cfg.CreateMap<UserInventoryModel, UserInventoryResponse>();
                cfg.CreateMap<UserModel, UserResponse>();
                cfg.CreateMap<UserTypeModel, UserTypeResponse>();
                cfg.CreateMap<WeeklyScheduleModel, WeeklyScheduleResponse>();

            });
            _mapper = config.CreateMapper();
            _userService = userService;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/<UserController>
        [HttpGet("userTypes")]
        public IEnumerable<string> GetUserTypes()//return user type table id,description 
        {

            return new string[] { "value1", "value2" };
        }
        //GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id<=0)
            {
                return BadRequest();
            }
            var getUser = _userService.GetUserById(id);
            if (getUser != null)
            {
                _userService.DeleteUser(id);
                return Ok();
            }
            return BadRequest("Record not found");
        }
        public static Login login = new Login();
        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<Login>> VerifayRegistor([FromBody] UserRequest userRequest)
        {
            CreatePasswordHash(userRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);

            login.Email = userRequest.Email;
            login.PasswordHash = passwordHash;
            login.PasswordSalt = passwordSalt;
            if(!VerifayPasswordHash(userRequest.Password, login.PasswordHash, login.PasswordSalt))
            {
                
            }
            return Ok(userRequest);

        }
        [HttpPost("userInfo")]
        public async Task<ActionResult<Login>> Registor([FromBody] UserInformationRequest userInformationRequest)
        {
            var userInfo = _mapper.Map<UserInformationRequest,UserInformationModel>(userInformationRequest);
            var addUserInfo = _userService.AddUserInformation(userInfo);
            return Ok(userInformationRequest);

        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserRequest userRequest)
        {
            if (login.Email != userRequest.Email)
            {
                return BadRequest("User not faund");
            }
            if (!VerifayPasswordHash(userRequest.Password, login.PasswordHash, login.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }
            return Ok("My Creyzi Token");
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifayPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
