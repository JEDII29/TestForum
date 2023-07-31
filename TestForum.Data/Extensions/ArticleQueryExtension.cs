using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestForum.Data.Entities;

namespace TestForum.Data.Extensions
{
	public static class ArticleQueryExtension
	{
		public static IEnumerable<ArticleEntity> GetArticlesById(this IQueryable<ArticleEntity> articleEntitie, Guid userId)
	=> articleEntitie.Where(r => r.UserId == userId);
		public static IEnumerable<ArticleEntity> GetNewestArticles(this IQueryable<ArticleEntity> articleEntitie, Guid userId)
	=> articleEntitie.OrderBy(r => r.PublicationTime).Take(10);
	}
}
