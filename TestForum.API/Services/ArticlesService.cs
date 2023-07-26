using TestForum.API.Abstract;
using TestForum.API.Models;
using TestForum.Data;

namespace TestForum.API.Services
{
	public class ArticlesService : IArticlesService
	{
		private readonly ForumDbContext _dbContext;

		public ArticlesService(ForumDbContext dbContext)
		{
			_dbContext = dbContext;
		}

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