using TestForum.API.Models;

namespace TestForum.API.Abstract
{
	public interface IArticlesService
	{
		public Task<Article[]> GetTenNewestArticles();
		public Task<IEnumerable<Article>> GetAllUserArticles(User user);
		public Task PublishNewArticle(Article article);
		public Task ChangeArticleStatus();
	}
}
