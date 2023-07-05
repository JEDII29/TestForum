using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using TestForum.Data.Entities;

namespace TestForum.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		public DbSet<UserEntity> Users { get; set; }
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
				.HasForeignKey(c => c.UserId);

			modelBuilder.Entity<ArticleEntity>()
				.HasMany(a => a.Comments)
				.WithOne(c => c.Article)
				.HasForeignKey(c => c.ArticleId);
		}
	}
}


