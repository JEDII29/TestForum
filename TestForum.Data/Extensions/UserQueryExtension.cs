using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestForum.Data.Entities;

namespace TestForum.Data.Extensions
{
	public static class UserQueryExtension
	{
		public static string GetAuthorName(this IQueryable<UserEntity> userEntitie, Guid userId)
			=> userEntitie.Where(r => r.Id == userId).Select(r => r.UserName).FirstOrDefault();
	}
}
