using TestForum.Data.Entities;

namespace TestForum.API.Abstract
{
	public interface IReputationService
	{
		public Task ChangeRepAfterCommentVote(Guid voterId, Guid targetId, VoteType vote);
		public Task ChangeRepAfterArticleVote(Guid voterId, Guid targetId, VoteType vote);
		public Task ChangeRepAfterArticleAdd(Guid userId);
		public Task ChangeRepAfterCommentAdd(Guid userId);
	}
}
