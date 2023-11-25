using TestForum.Data.Entities;

namespace TestForum.API.Requests
{
	public class VoteRequest
	{
		public Guid TargetId { get; set; }
		public VoteType Type { get; set; }
	}
}
