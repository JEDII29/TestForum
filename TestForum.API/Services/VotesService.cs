using TestForum.API.Abstract;
using TestForum.API.Infrastructure;
using TestForum.API.Models;
using TestForum.Data;
using TestForum.Data.Entities;

namespace TestForum.API.Services
{
	public class VotesService : IVotesService
	{
		private readonly ForumDbContext _dbContext;
		private readonly MapperProfile _mapper;

		public VotesService(ForumDbContext dbContext, MapperProfile mapper) 
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<bool> CheckExistingVotes(VoteDTO vote)
		{
			var existingVote = _dbContext.Votes.Where(v => v.UserId == vote.UserId && v.TargetId == vote.TargetId).FirstOrDefault();
			if (existingVote != null)
				return true;
			else
				return false;

		}
		public Task AddVote(VoteDTO vote)
		{
			if (!CheckExistingVotes(vote).Result)
			{
				VoteEntity voteToDb = _mapper.MapToEntity(vote);
				_dbContext.Votes.Add(voteToDb);
				_dbContext.SaveChanges();
			}
			return Task.CompletedTask;
		}



		public Task DeleteVote(VoteDTO vote)
		{
			if (CheckExistingVotes(vote).Result)
			{
				var toDelete = _dbContext.Votes.Where(v => v.UserId == vote.UserId && v.TargetId == vote.TargetId).FirstOrDefault();
				if (toDelete != null)
				{
					_dbContext.Votes.Remove(toDelete);
				}
				_dbContext.SaveChanges();
			}
			return Task.CompletedTask;
		}
	}
}
