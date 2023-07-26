using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForum.Data.Entities
{
	public class ApplicationUserEntity : IdentityUser<Guid>
	{ 
		[StringLength(50, MinimumLength = 5)]
		public override string UserName { get; set; }
		public int Reputation { get; private set; }

		public virtual ICollection<ArticleEntity> Articles { get; set; }

		public virtual ICollection<CommentEntity> Comments { get; set; }

		//public virtual ICollection<IdentityRole> Roles { get; set; }
	}
}
