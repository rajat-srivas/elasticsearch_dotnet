using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ElasticSearch_DotNet_Demo.Models
{
	public class Tweet: EntityBase
	{
		public List<string>? Edit_History_Tweet_Ids { get; set; }

		public string? Text { get; set; }


	}

	public class TweetData
	{
		public List<Tweet>? Data { get; set; }
	}
	public class TwitterUser : EntityBase
	{
		public string? Name { get; set; }

		public string? Username { get; set; }
	}

	public class UserData
	{
		public TwitterUser? Data { get; set; }
	}
}
