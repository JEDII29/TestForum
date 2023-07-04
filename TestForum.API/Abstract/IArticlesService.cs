using TestForum.API.Models;

namespace TestForum.API.Abstract
{
	public interface IArticlesService
	{
		public Task<Article[10]> GetTenNewestArticles();
		public Task<IEnumerable<Article>> GetAllUserArticles(User user);
		public Task PublishNewArticle(Article article);
		public Task ChangeArticleStatus();
	}
}
