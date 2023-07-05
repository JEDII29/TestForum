using Microsoft.AspNetCore.Http.HttpResults;
using TestForum.API.Abstract;
using TestForum.API.Models;

namespace TestForum.API.Services
{
	public class ExampleUsersService : IUsersService
	{
		public ExampleUsersService() { }

		public async Task<IEnumerable<User>> GetAllUsers()
		{
			//throw new NotImplementedException();
			List<User> users = new()
			{
				new User ("chuj", "haslo"),
				new User ("debil", "maslo"),
				new User ("karpik", "paslo"),
			};

			foreach (var user in users) { 
				user.SetRandomReputation(); 
			}

			return await Task.FromResult(users);

		}

		public void ChangeUserReputation(int value, User user)
		{
			user.ChangeReputation(value);
		}
	}
}
