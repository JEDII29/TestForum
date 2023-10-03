using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TestForum.API.Abstract;
using TestForum.API.Infrastructure;
using TestForum.API.Models;
using TestForum.API.Requests;
using TestForum.Data;
using TestForum.Data.Entities;

namespace TestForum.API.Services
{
	public class DbUserService : IUsersService
	{
		private readonly ForumDbContext _dbContext;
		private readonly MapperProfile _mapper;
		private readonly UserManager<UserEntity> _userManager;

		public DbUserService(ForumDbContext dbContext, MapperProfile mapper, UserManager<UserEntity> userManager)
		{
			_dbContext = dbContext;
			_mapper = mapper;
			_userManager = userManager;
		}

		public void ChangeUserReputation(int value, UserDTO user)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<UserDTO>> GetAllUsers()
		{
			var users = _mapper.MapToDTO(_userManager.Users.ToList());
			return users;
		}

		public async Task SaveNewUser(UserRequest userRequest)
		{
			var newUser = new UserEntity(userRequest.UserName);
			await _userManager.CreateAsync(newUser, password: userRequest.Password);
			await _userManager.AddToRoleAsync(newUser, "user");
		}
	}
}
