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
		[Required]
		public Guid Id { get; set; }
		[Required]
		public string Content { get; set; }
		[Required]
		public DateTime PublicationTime { get; set; }
		[Required]
		[ForeignKey("UserId")]
		public UserEntity User { get; set; }
		public Guid UserId { get; set; }
		[Required]
		[ForeignKey("ArticleId")]
		public ArticleEntity Article { get; set; }

		public Guid ArticleId { get; set; }

		public virtual ICollection<VoteEntity>? Votes { get; set; }
	}
}
