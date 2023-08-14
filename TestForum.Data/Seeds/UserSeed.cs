using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestForum.Data;
using TestForum.Data.Entities;

namespace TestForum.Data.Seeds
{
	public static class UserSeed
	{
		public static void SeedUsers(IServiceProvider serviceProvider)
		{
			using (var context = new ForumDbContext(
				serviceProvider.GetRequiredService<DbContextOptions<ForumDbContext>>()))
			{
				if (context.Users.Any())
				{
					// Users already seeded
					return;
				}

				var passwordHasher = serviceProvider.GetRequiredService<IPasswordHasher<UserEntity>>();

				var user1 = new UserEntity("user1", passwordHasher.HashPassword(null, "password1"))
				{
					Reputation = 100
				};

				var user2 = new UserEntity("user2", passwordHasher.HashPassword(null, "password2"))
				{
					Reputation = 150
				};

				var user3 = new UserEntity("user3", passwordHasher.HashPassword(null, "password3"))
				{
					Reputation = 75
				};

				context.Users.AddRange(user1, user2, user3);
				context.SaveChanges();
			}
		}
	}
}