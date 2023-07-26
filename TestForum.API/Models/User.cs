using Microsoft.AspNetCore.Identity;

namespace TestForum.API.Models
{
	public class User : IdentityUser
	{
		public Guid Id { get; set; }
		public string Nickname { get; set; }
		public string Password { get; set; }
		public int Reputation { get; private set; }
		public List<Article> PublishedArticles { get; set;}

		public User() { }

		public User(string nickname, string password) 
		{ 
			Nickname = nickname;
			Password = password;
			Reputation = 0;
		}

		public void SetRandomReputation()
		{
			Random random = new Random();
			Reputation = random.Next(0, 40);
			
		}

		public void ChangeReputation(int value)
		{
			Reputation = value;
		} 
	}
}
