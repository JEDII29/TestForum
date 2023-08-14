using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForum.Data.Entities
{
	public class UserEntity : IdentityUser<Guid>
	{
		[Key]
		[Required]
		public override Guid Id { get; set; }
		[StringLength(50, MinimumLength = 5)]
		[Required]
		public override string? UserName { get; set; }
		[Required]
		public int Reputation { get; set; }
		[Required]
		public override string? PasswordHash { get; set; }
		[NotMapped]
		public override string? Email { get; set; }
		[NotMapped]
		public override string? NormalizedEmail { get; set; }
		[NotMapped]
		public override bool EmailConfirmed { get; set; }
		[NotMapped]
		public override DateTimeOffset? LockoutEnd {get;set;}
		[NotMapped]
		public override bool LockoutEnabled { get; set; }
		[NotMapped]
		public override int AccessFailedCount { get; set; }

		public UserEntity(string userName, string passwordHash) : base(userName)
		{
			PasswordHash = passwordHash;
			Reputation = 0;
		}

		public virtual ICollection<ArticleEntity>? Articles { get; set; }

		public virtual ICollection<CommentEntity>? Comments { get; set; }
	}
}
