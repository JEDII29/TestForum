using TestForum.API.Models;

namespace TestForum.API.Abstract
{
	public interface IVotesService
	{
		Task AddVote(VoteDTO vote);
		Task<bool> CheckExistingVotes(VoteDTO vote);
		Task DeleteVote(VoteDTO vote);

	}
}
