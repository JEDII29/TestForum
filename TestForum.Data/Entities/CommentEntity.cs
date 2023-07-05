using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForum.Data.Entities
{
	public class CommentEntity
	{
		[Key]
		public Guid Id { get; set; }
		public string Content { get; set; }

		public DateTime PublicationTime { get; set; }

		[ForeignKey("UserId")]
		public UserEntity User { get; set; }

		public Guid UserId { get; set; }

		[ForeignKey("ArticleId")]
		public ArticleEntity Article { get; set; }

		public Guid ArticleId { get; set; }
	}
}
