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
        private IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserInformationRequest, UserInformationModel>();
                cfg.CreateMap<UserRequest, UserModel>();
                cfg.CreateMap<UserTypeRequest, UserTypeModel>();
                cfg.CreateMap<UserInformationModel, UserInformationResponse>();
                cfg.CreateMap<UserModel, UserResponse>();
                cfg.CreateMap<UserTypeModel, UserTypeResponse>();
            });
            _mapper = config.CreateMapper();
            _userService = userService;
            _configuration = configuration;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<UserResponse> Get()
        {
            var userModel = _userService.GetAllUsers().ToList();
            return _mapper.Map<List<UserModel>,List<UserResponse>>(userModel);
        }

        // GET: api/<UserController>
        [HttpGet("userTypes")]
        public IEnumerable<UserTypeResponse> GetUserTypes()//return user type table id,description 
        {
            var userTypes = _userService.GetAllUserTypes().ToList();
            return _mapper.Map<List<UserTypeModel>,List<UserTypeResponse>>(userTypes);
        }
        //GET api/<UserController>/5
        [HttpGet("{id}")]
        public UserResponse Get(int id)
        {
            var user = _userService.GetUserById(id);
            return _mapper.Map<UserModel,UserResponse>(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UserRequest userRequest)
        {
            var user = _mapper.Map<UserRequest,UserModel>(userRequest);
            var modifyUser = _userService.ModifyUser(user, id);
            if (modifyUser)
            {
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetUserById(id);
            if(user != null)
            {
                _userService.DeleteUser(id);
            }
            return BadRequest();
        }
        

        // POST api/<UserController>       

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login([FromBody] UserRequest userRequest)
        {
            var userModel = _mapper.Map<UserRequest,UserModel>(userRequest);
            return await _userService.Login(userModel, _configuration);
        }


        [HttpPost("verifyRegistration")]
        public async Task<ActionResult> VerifyRegistration([FromBody] UserRequest userRequest)
        {
            var userModel = _mapper.Map<UserRequest, UserModel>(userRequest);
            var result = await _userService.Registration(userModel, _configuration);
            if (!result)
            {
                    
            }
            return Ok();
        }



        [HttpPost("registration")]
        public async Task<ActionResult<int>> Registration([FromBody] UserInformationRequest userInformationRequest)
        {            
            var userInformationModel = _mapper.Map<UserInformationRequest, UserInformationModel>(userInformationRequest);
            return await _userService.UserInformationRegistration(userInformationModel);           
        }
    }
}
