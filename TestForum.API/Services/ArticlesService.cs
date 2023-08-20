using AutoMapper;
using TestForum.API.Abstract;
using TestForum.API.Mappers;
using TestForum.API.Models;
using TestForum.Data;
using TestForum.Data.Extensions;

namespace TestForum.API.Services
{
	public class ArticlesService : IArticlesService
	{
		private readonly ForumDbContext _dbContext;
		private readonly ArticleMapper _mapper;

		public ArticlesService(ForumDbContext dbContext, ArticleMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public Task ChangeArticleStatusById(Guid articleId)
		{
			throw new NotImplementedException();
		}

		public Task<ArticleDTO> GetArticleById(Guid articleId)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<ArticleDTO>> GetTenNewestArticles()
		{
			var articles = _dbContext.Articles.GetNewestArticles().ToList();
			var articleDTOs = _mapper.MapToDTO(articles);
			var result = articleDTOs.Where(x => x.AuthorName == null)
				.Select(x => { x.AuthorName = _dbContext.Users.GetAuthorName(x.UserId);
					return x;
				});
			return Task.FromResult(result);
		}

		public Task<IEnumerable<ArticleDTO>> GetUserArticles(Guid userId)
		{
			var articles = _dbContext.Articles.GetArticlesById(userId).ToList();
			var articleDTOs = _mapper.MapToDTO(articles);
			var result = articleDTOs.Where(x => x.AuthorName == null)
				.Select(x => {
				x.AuthorName = _dbContext.Users.GetAuthorName(x.UserId);
					return x;
				});
			return Task.FromResult(result);
		}

		public Task PublishNewArticle(ArticleDTO article)
		{
			var articleEntity = _mapper.MapToEntity(article);
			_dbContext.Add(articleEntity);
			_dbContext.SaveChangesAsync();
			return Task.CompletedTask;
		}
	}
}