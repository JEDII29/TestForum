using TestForum.Data.Entities;

namespace TestForum.API.Abstract
{
	public interface IAuthenticationService
	{
		Task<string> GenerateAccessToken(UserEntity user);
	}
}
