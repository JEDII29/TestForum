using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TestForum.API.Abstract;
using TestForum.API.Models;

namespace TestForum.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ArticleController : Controller
	{
		private readonly IArticlesService _articleService;

		public ArticleController(IArticlesService articleService)
		{
			_articleService = articleService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[Authorize (Roles = "user")]
		[HttpGet("GetArticles")]
		public async Task<IActionResult>  GetArticles()
		{
			var articles = await _articleService.GetTenNewestArticles();
			if (articles.Any())
				return Ok(articles);
			return NotFound();
		}

		[HttpGet("GetUserArticles")]
		public async Task<IActionResult> GetUserArticles(string requestedId)
		{
			Guid userId = new Guid(requestedId); 
			var articles = await _articleService.GetUserArticles(userId);
			if (articles.Any())
				return Ok(articles);
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> PostNewArticle([FromBody]ArticleDTO newArticle)
		{
			await _articleService.PublishNewArticle(newArticle);
			return Ok();
		}
	}
}
