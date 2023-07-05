using TestForum.API.Abstract;
using TestForum.API.Models;

namespace TestForum.API.Services
{
	public class ArticlesService : IArticlesService
	{

		public Task ChangeArticleStatus()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Article>> GetAllUserArticles(User user)
		{
			// Asynchroniczne operacje
			throw new NotImplementedException();
		}

		public Task<Article> GetTenNewestArticles()
        {
			throw new NotImplementedException();
		}

		public Task PublishNewArticle(Article article)
		{
			throw new NotImplementedException();
		}

		Task<Article[]> IArticlesService.GetTenNewestArticles()
		{
			throw new NotImplementedException();
		}
	}
}