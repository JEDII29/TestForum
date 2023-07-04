using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForum.Data.Entities
{
	public class UserEntity
	{
		[Key]
		public Guid Id { get; set; }
		[StringLength(50, MinimumLength = 3)]
		public string Nickname { get; set; }
		[StringLength(50, MinimumLength = 5)]
		public string Password { get; set; }
		public int Reputation { get; private set; }

		public virtual ICollection<ArticleEntity> Articles { get; set; }

		public virtual ICollection<CommentEntity> Comments { get; set; }
	}
}
