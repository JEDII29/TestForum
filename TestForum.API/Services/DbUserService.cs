using TestForum.API.Abstract;
using TestForum.API.Models;
using TestForum.Data;

namespace TestForum.API.Services
{
	public class DbUserService : IUsersService
	{
		private readonly ForumDbContext _dbContext;

		public DbUserService(ForumDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void ChangeUserReputation(int value, UserDTO user)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<UserDTO>> GetAllUsers()
		{
			throw new NotImplementedException();
		}

		public void SaveNewUser(UserDTO user)
		{
			throw new NotImplementedException();
		}
	}
}
