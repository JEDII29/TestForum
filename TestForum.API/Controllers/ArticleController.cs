using Microsoft.AspNetCore.Mvc;

namespace TestForum.API.Controllers
{
	[Route("api/[controller]")]
	public class ArticleController : Controller
	{
		private readonly IArticleService _articleService;

		ArticleController(IArticleService articleService)
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
			if(articles.Any())
				return Ok(articles)
			return NotFound();
		}
		[HttpGet("GetUserArticles")]
		public async Task<IActionResult>  GetUserArticles(int )
		{
			var articles = await _articleService.GetAllUserArticles();
			if(articles.Any())
				return Ok(articles)
			return NotFound();
		}
	}
}
