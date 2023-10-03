using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForum.Data.Entities
{
	public enum VoteType { plus, minus}
	public class VoteEntity
	{
		[Key]
		[Required]
		public Guid Id { get; set; }

		[Required]
		[ForeignKey("UserId")]
		public Guid UserId { get; set; }

		[Required]
		[ForeignKey("TargetId")]
		public Guid TargetId { get; set; }

		[Required]
		public VoteType Type { get; set; }

	}
}
