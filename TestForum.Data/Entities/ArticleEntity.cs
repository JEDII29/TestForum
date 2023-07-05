using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForum.Data.Entities
{
	public class ArticleEntity
	{

		[Key]
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public string Author { get; set; }
		public DateTime PublicationTime { get; set; }
		[ForeignKey("UserId")]
		public UserEntity User { get; set; }

		public Guid UserId { get; set; }

		public virtual ICollection<CommentEntity> Comments { get; set; }
	}
}
