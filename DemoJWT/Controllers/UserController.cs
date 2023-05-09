using DemoJWT.Models;
using DemoJWT.Tools;
using DemoJWT_DAL.Entities;
using DemoJWT_DAL.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TokenManager _tokenManager;

        public UserController(IUserService userService, TokenManager tokenManager)
        {
            _userService = userService;
            _tokenManager = tokenManager;
        }

        [HttpPost]
        public IActionResult Login(LoginForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            User connectedUser = _userService.Login(form.Email, form.Password);
            string token = _tokenManager.GenerateToken(connectedUser);

            return Ok(token);
        }


        //[Authorize("userPolicy")]
        //[HttpGet("{id}")]
        //public IActionResult GetProfile(int id)
        //{
        //    return Ok();
        //}

        [Authorize("userPolicy")] //Admin et modo peuvent accéder
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [Authorize("adminPolicy")] //uniquement accessible a Admin
        [HttpPatch("{id}")]
        public IActionResult SwitchRole(int id)
        {
            _userService.SwitchRole(id);
            return Ok();
        }
    }
}
