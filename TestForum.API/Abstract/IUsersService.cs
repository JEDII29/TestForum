using TestForum.API.Models;

namespace TestForum.API.Abstract
{
	public interface IUsersService
	{
		public Task<IEnumerable<User>> GetAllUsers();
		public void ChangeUserReputation(int value, User user);
	}
}
