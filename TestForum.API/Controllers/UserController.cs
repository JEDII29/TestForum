using Microsoft.AspNetCore.Mvc;
using TestForum.API.Models;
using TestForum.API.Abstract;

namespace TestForum.API.Controllers
{
	[Route("api/[controller]")]
	public class UserController : Controller
	{
		private readonly IUsersService _userGetter;
		public UserController(IUsersService userGettter) { 
			_userGetter = userGettter;
		}

		//public IActionResult Index()
		//{
		//	return View();
		//}

		[HttpGet]
		public async Task<IActionResult> GetAll() 
		{
			var users = await _userGetter.GetAllUsers();
			if (users.Any()) {
				return Ok(users);
			}
			return NotFound();
		}
	}
}
