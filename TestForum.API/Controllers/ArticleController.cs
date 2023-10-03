using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;
using TestForum.API.Abstract;
using TestForum.API.Models;
using TestForum.API.Requests;

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

		[Authorize(Roles = "user")]
		[HttpPost]
		public async Task<IActionResult> PostNewArticle([FromBody]ArticleRequest newArticle)
		{
			var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
			Guid newId = Guid.NewGuid();
			ArticleDTO articleDTO = new ArticleDTO(newId, newArticle.Title, newArticle.Content, userId, DateTime.Now);
			await _articleService.PublishNewArticle(articleDTO);
			return Ok();
		}
	}
}
