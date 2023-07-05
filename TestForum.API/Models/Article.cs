﻿namespace TestForum.API.Models
{
	public class Article
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public int AuthorID { get; set; }
		public string AuthorName {get; set; }
		public DateTime PublishionDate {get; set; }
		public State Status {get; set; } 
		public Article() { }
	}
}
