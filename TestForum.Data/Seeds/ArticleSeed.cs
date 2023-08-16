using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using TestForum.Data.Entities;

namespace TestForum.Data.Seeds
{
	public static class ArticleSeed
	{
		public static void SeedArticles(IServiceProvider serviceProvider)
		{
			using (var context = new ForumDbContext(
				serviceProvider.GetRequiredService<DbContextOptions<ForumDbContext>>()))
			{
				if (context.Articles.Any())
				{
					// Users already seeded
					return;
				}
				List<Guid> usersId = context.Users.Select(user => user.Id).ToList();
				var article1 = new ArticleEntity()
				{
					Title = "firstArticle",
					Content = "Czy Kostek dzisiaj marudzi?",
					UserId = usersId.First()
				};
				var article2 = new ArticleEntity()
				{
					Title = "secondArticle",
					Content = "Lubie sobie zjesc (Tusio).",
					UserId = usersId[1]
				};
				var article3 = new ArticleEntity()
				{
					Title = "thirdArticle",
					Content = "18 procent popuacji jest odpowiedzialne za 52% przestępstw. Change my mind",
					UserId = usersId[1]
				};
				var article4 = new ArticleEntity()
				{
					Title = "fourthArticle",
					Content = "to jest czwarty artykuł",
					UserId = usersId.First()
				};
				context.Articles.AddRange(article1, article2, article3, article4);
				context.SaveChanges();
			}
		}
	}
}