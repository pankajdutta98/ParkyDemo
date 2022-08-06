using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyWebAPI.Models;
using ParkyWebAPI.Repository.IRepository;

namespace ParkyWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository IuserRepo)
        {
            _userRepo = IuserRepo;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] User Model)
        {
            var user = _userRepo.authenticate(Model.userName, Model.password);
            if (user == null)
            {
                return BadRequest(new { message = "Username Or password is incorrect" });
            }
            else
            {
                return Ok(user);
            }
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] User Model)
        {
            bool userExist = _userRepo.userExist(Model.userName);

            if (userExist)
            {
                return BadRequest(new { message = "Username already in use." });
            }
            else
            {
                if(Model.userName.Contains(' '))
                {
                    return BadRequest(new { message = "Invalid Username" });
                }
                var user = _userRepo.addUser(Model.userName,Model.password);
                return Ok(User);
            }
        }
        [AllowAnonymous]
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            List<User> userList = new List<User>();
            userList = _userRepo.getUsers();
            if (userList != null && userList.Count > 0)
            {
                return Ok(userList);
            }
            else
            {
                return BadRequest(new { message = "No Users Found" });
            }

        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] User Model)
        {
            var user = _userRepo.authenticate(Model.userName, Model.password);
            if (user == null)
            {
                return BadRequest(new { message = "Username Or password is incorrect" });
            }
            else
            {
                return Ok(user);
            }

        }
        [Authorize]
        [HttpGet("GetUser")]
        public IActionResult GetUser(int id)
        {
            User userList = new User();
            userList = _userRepo.getUser(id);
            if (userList != null)
            {
                return Ok(userList);
            }
            else
            {
                return BadRequest(new { message = "User Not Found" });
            }

        }
    }
}
