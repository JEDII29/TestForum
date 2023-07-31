using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using TestForum.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TestForum.Data
{
	public class ForumDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
	{
		public ForumDbContext(DbContextOptions<ForumDbContext> options)
			: base(options)
		{
		}
		public DbSet<ArticleEntity> Articles { get; set; }
		public DbSet<CommentEntity> Comments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserEntity>()
				.HasMany(u => u.Articles)
				.WithOne(a => a.User)
				.HasForeignKey(a => a.UserId);

			modelBuilder.Entity<UserEntity>()
				.HasMany(u => u.Comments)
				.WithOne(c => c.User)
				.HasForeignKey(c => c.UserId)
			    .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ArticleEntity>()
				.HasMany(a => a.Comments)
				.WithOne(c => c.Article)
				.HasForeignKey(c => c.ArticleId);

			modelBuilder.Ignore<IdentityUserLogin<Guid>>();

			modelBuilder.Entity<IdentityUserRole<Guid>>()
				.HasKey(r => new { r.UserId, r.RoleId });

			modelBuilder.Entity<IdentityUserToken<Guid>>()
				.HasKey(r => r.UserId);

		}
	}
}


