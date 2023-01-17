using System.ComponentModel.DataAnnotations;

namespace ElasticSearch_DotNet_Demo.Models
{
	public class EntityBase
	{
		[Key]
		public string Id { get; set; }
	}
}
