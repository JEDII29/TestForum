using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TestForum.API.Enums;

namespace TestForum.API.Models
{
	public class ArticleDTO
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public Guid UserId { get; set; }
		public string? AuthorName {get; set; }
		public DateTime PublicationTime {get; set; }
		public ArticleDTO() { }
		public ArticleDTO(Guid id, string title, string content, Guid userId, DateTime publicationTime)
		{
			Id = id;
			Title = title;
			Content = content;
			UserId = userId;
			PublicationTime = publicationTime;
		}
	}
}
