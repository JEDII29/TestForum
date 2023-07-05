using TestForum.API.Abstract;
using TestForum.API.Models;

namespace TestForum.API.Services
{
	public class ArticlesService : IArticlesService
	{
		public ExampleUsersService() { }

		public async Task<IEnumerable<Article>> GetAllUserArticles(User user)
		{
			throw new NotImplementedException();
		}

        public async Task<Article> GetTenNewestArticles()
        {
            throw new NotImplementedException();
        }
    }
}