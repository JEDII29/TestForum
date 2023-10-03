using Microsoft.AspNetCore.Mvc;
using TestForum.API.Models;
using TestForum.API.Abstract;
using Microsoft.AspNetCore.Identity;
using TestForum.API.Requests;
using TestForum.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace TestForum.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : Controller
	{
		private readonly IUsersService _userService;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserController(IUsersService userGettter) {
			_userService = userGettter;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[Authorize (Roles = "admin")]
		[Route("GetAll")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var users = await _userService.GetAllUsers();
			if (users.Any())
			{
				return Ok(users);
			}
			return NotFound();
		}

		[Route("SignIn")]
		[HttpPost]
		public async Task<IActionResult> SignInNewUser(UserRequest newUser)
		{
			try
			{
				await _userService.SaveNewUser(newUser);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
