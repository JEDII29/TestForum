using Microsoft.AspNetCore.Identity;
using TestForum.API.Abstract;
using TestForum.Data;
using TestForum.Data.Entities;

namespace TestForum.API.Services
{
	public class ReputationService : IReputationService
	{
		private readonly UserManager<UserEntity> _userManager;
		private readonly ForumDbContext _dbContext;

		public ReputationService(UserManager<UserEntity> userManager, ForumDbContext dbContext)
		{
			_userManager = userManager;
			_dbContext = dbContext;
		}

		public async Task ChangeRepAfterArticleAdd(Guid userId)
		{
			var User = await _userManager.FindByIdAsync(userId.ToString());;
			User.Reputation += 1;
		}
		public async Task ChangeRepAfterCommentAdd(Guid userId)
		{
			var User = await _userManager.FindByIdAsync(userId.ToString());
			User.Reputation += 2;
		}

		public async Task ChangeRepAfterArticleVote(Guid voterId, Guid targetId, VoteType vote)
		{
			var Voter = await _userManager.FindByIdAsync(voterId.ToString());
			var receiverId = _dbContext.Articles.Where(r => r.Id == targetId).Select(r => r.UserId);
			var Receiver = await _userManager.FindByIdAsync(receiverId.ToString());

			if (vote == VoteType.plus)
			{
				Voter.Reputation -= 1;
				Receiver.Reputation += 20;
			}
			else
			{
				Voter.Reputation -= 1;
				Receiver.Reputation -= 5;
			}

		}

		public async Task ChangeRepAfterCommentVote(Guid voterId, Guid targetId, VoteType vote)
		{
			var Voter = await _userManager.FindByIdAsync(voterId.ToString());
			var receiverId = _dbContext.Articles.Where(r => r.Id == targetId).Select(r => r.UserId);
			var Receiver = await _userManager.FindByIdAsync(receiverId.ToString());

			if (vote == VoteType.plus)
			{
				Voter.Reputation -= 1;
				Receiver.Reputation += 10;
			}
			else
			{
				Voter.Reputation -= 1;
				Receiver.Reputation -= 5;
			}

		}
	}
}
