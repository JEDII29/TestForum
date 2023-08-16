﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TestForum.API.Models
{
	public class UserDTO : IdentityUser<Guid>
	{
		public override Guid Id { get; set; }
		public override string UserName { get; set; }
		public int Reputation { get; private set; }
		public List<ArticleDTO>? PublishedArticles { get; set; }

		public UserDTO(string userName) 
		{ 
			UserName = userName;
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