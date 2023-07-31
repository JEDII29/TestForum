using TestForum.API.Models;

namespace TestForum.API.Abstract
{
	public interface IUsersService
	{
		public Task<IEnumerable<UserDTO>> GetAllUsers();
		public void ChangeUserReputation(int value, UserDTO user);
		public void SaveNewUser(UserDTO user);
	}
}
