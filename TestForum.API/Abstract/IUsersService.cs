using TestForum.API.Models;

namespace TestForum.API.Abstract
{
	public interface IUsersService
	{
		public Task<IEnumerable<User>> GetAllUsers();
	}
}
