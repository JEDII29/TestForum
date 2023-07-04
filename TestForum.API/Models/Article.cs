using Microsoft.Identity.Client;

namespace TestForum.API.Models
{
	public class Article
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Author { get; set; }
		public DateTime PublicationTime { get; set; }
		public List<Comment> Comments { get; set; }
		public Article() { }
	}
}
