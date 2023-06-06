namespace TestForum.API.Models
{
	public class User
	{
		public string? Nickname { get; set; } = null;
		public string? Password { get; set; }
		public int Reputation { get; private set; }
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
	}
}
