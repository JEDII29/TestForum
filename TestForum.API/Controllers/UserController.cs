using Microsoft.AspNetCore.Mvc;
using TestForum.API.Models;
using TestForum.API.Abstract;
using Microsoft.AspNetCore.Identity;

namespace TestForum.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : Controller
	{
		private readonly IUsersService _userGetter;
		private readonly UserManager<UserDTO> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		
		public UserController(IUsersService userGettter, UserManager<UserDTO> userManager, RoleManager<IdentityRole> roleManager) { 
			_userGetter = userGettter;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[Route("GetAll")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var users = await _userGetter.GetAllUsers();
			if (users.Any())
			{
				return Ok(users);
			}
			return NotFound();
		}
	}
}
