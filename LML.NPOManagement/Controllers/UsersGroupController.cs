using Amazon.S3;
using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Bll.Services;
using LML.NPOManagement.Request;
using LML.NPOManagement.Response;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LML.NPOManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersGroupController : ControllerBase
    {
        private IMapper _mapper;
        private IUsersGroupService _usersGroupService;
        private IConfiguration _configuration;
        private IAmazonS3 _s3Client;
        public UsersGroupController(IUsersGroupService usersGroupService, IConfiguration configuration, IAmazonS3 s3Client)
        {
            var config = new MapperConfiguration(cfg =>
            {              
                cfg.CreateMap<UserInformationRequest, UserInformationModel>();
                cfg.CreateMap<UserRequest, UserModel>();
                cfg.CreateMap<UserInformationModel, UserInformationResponse>();
                cfg.CreateMap<UserModel, UserResponse>();
                cfg.CreateMap<UsersGroupRequest,UsersGroupModel>();
                cfg.CreateMap<UsersGroupModel, UsersGroupResponse>();
            });
            _mapper = config.CreateMapper();
            _usersGroupService = usersGroupService;
            _configuration = configuration;
            _s3Client = s3Client;
        }
        // POST api/<UsersGroupController>
        [HttpPost("group")]
        public async Task<ActionResult<UsersGroupResponse>> Post([FromBody] CombineUsersRequest request)
        {
            var usersGroupModel = _mapper.Map<UsersGroupRequest, UsersGroupModel>(request.UsersGroupRequest);

            var newUsersGroupModel = await _usersGroupService.AddUsersGroup(usersGroupModel, request.UserIds);

            if (newUsersGroupModel == null)
            {
                return BadRequest("Your Request Is Not Valid");
            }

            var newUsersGroupResponse = _mapper.Map<UsersGroupModel, UsersGroupResponse>(newUsersGroupModel);

            return Ok(newUsersGroupResponse);
        }

        [HttpGet("byFirstChars/{userName}")] // Api Endpoint correction ? 
        public async Task<ActionResult<List<UserResponse>>> GetByFirstChars(string userName, bool showGroupsOnly)
        {
            var user = HttpContext.Items["User"] as UserModel; // How it Works
            var users = await _usersGroupService.GetUserByUsername(userName, showGroupsOnly);
            if (users == null)
            {
                return NotFound("Users Not Found");
            }
            return Ok(_mapper.Map<List<UserModel>, List<UserResponse>>(users));
        }
    }
}
