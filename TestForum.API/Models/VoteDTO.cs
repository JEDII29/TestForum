using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TestForum.Data.Entities;

namespace TestForum.API.Models
{
	public class VoteDTO
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid TargetId { get; set; }
		public VoteType Type { get; set; }

		public VoteDTO(Guid id, Guid userId, Guid targetId, VoteType type) 
		{
			Id = id;
			UserId = userId;
			TargetId = targetId;
			Type = type;

		}
	}
}