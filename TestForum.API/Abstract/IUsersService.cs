using TestForum.API.Models;
using TestForum.API.Requests;

namespace TestForum.API.Abstract
{
	public interface IUsersService
	{
		public Task<IEnumerable<UserDTO>> GetAllUsers();
		public void ChangeUserReputation(int value, UserDTO user);
		public Task SaveNewUser(UserRequest user);
	}
}
