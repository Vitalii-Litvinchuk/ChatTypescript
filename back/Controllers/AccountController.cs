using backback.Database;
using Core.Account.Models;
using Core.Account.ViewModels;
using Core.Services.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace back.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtService _jwtService;
        private readonly ChatDbContext _dbContext;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, JwtService jwtService, ChatDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                    return BadRequest("Password and confirm password do not match");
                User user = new User { Email = model.Email, Name = model.Name, Surname = model.Surname, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    await _signInManager.SignInAsync(user, false);
                else
                {
                    return BadRequest(result.Errors.ToArray());
                }
                return Created("Created", user);
            }
            return BadRequest(model);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result =
     await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                User user = await _userManager.FindByNameAsync(model.Email);
                return Ok(user.Id);
            }
            return BadRequest(result);
        }

        [HttpPost("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
