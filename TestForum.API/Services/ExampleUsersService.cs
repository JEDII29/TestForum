using Microsoft.AspNetCore.Http.HttpResults;
using TestForum.API.Abstract;
using TestForum.API.Models;

namespace TestForum.API.Services
{
	public class ExampleUsersService : IUsersService
	{
		private readonly HttpClient _httpClient;

		public ExampleUsersService(HttpClient httpClient) { 
			_httpClient = httpClient;
		}
		public ExampleUsersService() { }

		public async Task<IEnumerable<User>> GetAllUsers()
		{
			//throw new NotImplementedException();
			List<User> users = new List<User>
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

		//IEnumerable<User> IUsersService.GetAllUsers() => new List<User>() {
		//	new User {  
		//	},
		//	new User {  Nickname = "debil",
		//				Password = "maslo",
		//				Reputation = 2137 
		//	},
		//	new User {  Nickname = "karpik",
		//				Password = "paslo",
		//				Reputation = 420 
		//	}
		//};

	}
}
