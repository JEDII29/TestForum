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

		public Task<ArticleDTO[]> GetTenNewestArticles()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<ArticleDTO>> GetUserArticles(Guid userId)
		{
			var articles = _dbContext.Articles.GetArticlesById(userId).ToList();
			var articleDTO = _mapper.MapToDTO(articles);
			return Task.FromResult(articleDTO);
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