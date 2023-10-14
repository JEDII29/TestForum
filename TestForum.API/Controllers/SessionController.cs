using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestForum.API.Abstract;
using TestForum.API.Models;
using TestForum.Data.Entities;

namespace TestForum.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class SessionController : Controller
	{
		private readonly IAuthenticationService _authenticationService;
		public SessionController(IAuthenticationService authenticationService) 
		{
			_authenticationService = authenticationService;
		}

		[HttpGet("login")]
		public async Task<IActionResult> Login(SignInManager<UserEntity> signInManager, string login, string password)
		{
			var loginResult = await signInManager.PasswordSignInAsync(login, password, false, false);
			if (loginResult == Microsoft.AspNetCore.Identity.SignInResult.Success)
			{
				var user = await signInManager.UserManager.FindByNameAsync(login);
				//var token = await signInManager.UserManager.CreateSecurityTokenAsync(user);
				var token = await _authenticationService.GenerateAccessToken(user);
				return Ok(token);
			}
			return BadRequest();	
		}
	}
}
