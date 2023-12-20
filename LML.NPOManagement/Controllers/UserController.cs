using Amazon.S3;
using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Request;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        private IUserService _userService;
        private IConfiguration _configuration;

        public UserController(IUserService userService)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRequest, UserModel>();
                cfg.CreateMap<UserModel, UserResponse>();
            });
            _mapper = config.CreateMapper();
            _userService = userService;
        }

        [HttpGet("Search")]
        public async Task<List<UserInformationResponse>> SearchUser([FromQuery] string name)
        {
            var userRequest = new UserInformationModel()
            {
                FirstName = name,
                LastName = name,
            };

            var users = await _userService.GetUserByName(userRequest);
            if (users == null)
            {
                return null;
            }
            var newUsers = new List<UserInformationResponse>();
            foreach (var newUser in users)
            {
                var us = new UserInformationResponse
                {
                    UserId = newUser.UserId,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                };
                newUsers.Add(us);
            }
            return newUsers;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<UserResponse>> Get()
        {
            var userModel = await _userService.GetAllUsers();
            return _mapper.Map<List<UserModel>, List<UserResponse>>(userModel);
        }
        //GET: api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<UserResponse> Get(int id)
        {
            var user = await _userService.GetUserById(id);
            return _mapper.Map<UserModel, UserResponse>(user);
        }
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UserRequest userRequest)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return BadRequest();
            }
            var userModel = _mapper.Map<UserRequest, UserModel>(userRequest);
            var modifyUser = await _userService.ModifyUser(user, id);
            if (modifyUser)
            {
                return Ok();
            }
            return BadRequest();
        }
        // PUT api/<UserController>/5
        [HttpPut("userInfo")]
        public async Task<ActionResult> PutUserInfo(int id, [FromBody] UserInformationRequest userInformationRequest)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return BadRequest();
            }
            var userInfoModel = new UserInformationModel()
            {
                UserTypeEnum = userInformationRequest.UserTypeEnum,
                UserId = id,
                Gender = userInformationRequest.Gender,
                FirstName = userInformationRequest.FirstName,
                LastName = userInformationRequest.LastName,
                Metadata = userInformationRequest.UserMetadata,
                PhoneNumber = userInformationRequest.PhoneNumber,
                DateOfBirth = userInformationRequest.DateOfBirth,
            };
            var modifyUser = await _userService.ModifyUserInfo(userInfoModel, id);
            if (modifyUser)
            {
                return Ok();
            }
            return BadRequest();
        }
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user != null)
            {
                _userService.DeleteUser(id);
            }
            return BadRequest();
        }
    }
}
