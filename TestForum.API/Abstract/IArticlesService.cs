using TestForum.API.Models;

namespace TestForum.API.Abstract
{
	public interface IArticlesService
	{
		public Task<IEnumerable<ArticleDTO>> GetTenNewestArticles();
		public Task PublishNewArticle(ArticleDTO article);
		public Task<IEnumerable<ArticleDTO>> GetUserArticles(Guid userId);
		public Task<ArticleDTO> GetArticleById(Guid articleId);
		public Task ChangeArticleStatusById(Guid articleId);
	}
}
