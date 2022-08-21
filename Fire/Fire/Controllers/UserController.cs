using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fire.Models;
using Fire.Services.UserServices;
using Fire.ViewModels.User;
using Microsoft.AspNetCore.Authorization;

namespace Fire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }


        [HttpGet] 
        [ProducesResponseType(200, Type = typeof(ICollection<UserViewModels>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _userServices.GetAllUsers();
            return Ok(users);
        }


        [HttpGet("{id}")] 
        [ProducesResponseType(200, Type = typeof(UserViewModels))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userServices.GetUser(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
        [Authorize]
        [Route("getbylogin")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(UserViewModels))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserByLogin(string Login)
        {
            var user = await _userServices.GetUserByLogin(Login);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost] 
        public async Task<ActionResult> PostUser(InputUserViewModels inputModel)
        {

            var user = await _userServices.AddUser(inputModel);
            
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }


        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateUser(int id, EditUserViewModels editModel)
        {

            var user = await _userServices.UpdateUser(id, editModel);
            if (user == null) return BadRequest();
            return Ok(user);
        }

        [HttpDelete("{id}")] 
        public async Task<ActionResult<DeleteUserViewModels>> DeleteUser(int id)
        {

            var user = await _userServices.DeleteUser(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

    }
}
