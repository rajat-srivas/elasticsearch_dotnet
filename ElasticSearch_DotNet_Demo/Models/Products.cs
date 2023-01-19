using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ElasticSearch_DotNet_Demo.Models
{
	public class Feature
	{
		[JsonProperty("Outer Material")]
		public string? OuterMaterial { get; set; }

		[JsonProperty("Inner Material")]
		public string? InnerMaterial { get; set; }
		public string? Sole { get; set; }
		public string? Closure { get; set; }

		[JsonProperty("Heel Height")]
		public string? HeelHeight { get; set; }

		[JsonProperty("Heel Type")]
		public string? HeelType { get; set; }

		[JsonProperty("Shoe Width")]
		public string? ShoeWidth { get; set; }
	}

	public class Shoe
	{
		public string url { get; set; }
		public string title { get; set; }
		public string asin { get; set; }
		public string price { get; set; }
		public string brand { get; set; }
		public string product_details { get; set; }
		public string breadcrumbs { get; set; }
		public string[] images_list { get; set; }
		public Feature[] features { get; set; }

	}

	public class Shoes
	{
		[JsonProperty("shoes")]
		public IList<Shoe>? shoes { get; set; }
	}


}
