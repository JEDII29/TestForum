using Microsoft.AspNetCore.Http.HttpResults;
using TestForum.API.Abstract;
using TestForum.API.Models;
using TestForum.Data;
using TestForum.Data.Entities;

namespace TestForum.API.Services
{
	public class ExampleUsersService : IUsersService
	{

		public ExampleUsersService()
		{
		}

		public async Task<IEnumerable<UserDTO>> GetAllUsers()
		{
			//throw new NotImplementedException();
			List<UserDTO> users = new()
			{
				new UserDTO ("chuj"),
				new UserDTO ("debil"),
				new UserDTO ("karpik"),
			};

			foreach (var user in users) { 
				user.SetRandomReputation(); 
			}
			return await Task.FromResult(users);

		}

		public void ChangeUserReputation(int value, UserDTO user)
		{
			user.ChangeReputation(value);
		}

		public void SaveNewUser(UserDTO user)
		{
			throw new NotImplementedException();
		}
	}
}
