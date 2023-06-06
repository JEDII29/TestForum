using Microsoft.AspNetCore.Mvc;

namespace TestForum.API.Controllers
{
	[Route("api/[controller]")]
	public class ArticleController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
