using Microsoft.AspNetCore.Mvc;
using TestForum.API.Abstract;

namespace TestForum.API.Controllers
{
	[Route("api/[controller]")]
	public class ArticleController : Controller
	{
		private readonly IArticlesService _articleService;

		ArticleController(IArticlesService articleService)
		{
			_articleService = articleService;
		}
		// public IActionResult Index()
		// {
		// 	return View();
		// }
		[HttpGet("GetArticles")]
		public async Task<IActionResult>  GetArticles()
		{
			var articles = await _articleService.GetTenNewestArticles();
			if (articles.Any())
				return Ok(articles);
			return NotFound();
		}
		[HttpGet("GetUserArticles")]
		public Task<IActionResult>  GetUserArticles(int i)
		{
			//var articles = await _articleService.GetAllUserArticles();
			//if (articles.Any())
			//	return Ok(articles);
			//return NotFound();
			throw new NotImplementedException();
		}
	}
}
