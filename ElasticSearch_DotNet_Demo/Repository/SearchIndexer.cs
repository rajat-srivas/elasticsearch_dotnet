using ElasticSearch_DotNet_Demo.ElasticSearch;
using ElasticSearch_DotNet_Demo.Models;
using ElasticSearch_DotNet_Demo.Util;
using System.Text.Json;

namespace ElasticSearch_DotNet_Demo.Repository
{
	public class SearchIndexer : ISearchIndexer
	{
		TwitterClient _twtclient;
		ElasticClient _elasticClient;
		public SearchIndexer(TwitterClient twitterClient, ElasticClient elasticCLient)
		{
			_twtclient = twitterClient;
			_elasticClient = elasticCLient;
		}
		public async Task<bool> IndexTweetsByUserName(string userName)
		{
			string userId = await _twtclient.GetUserIdByUserName(userName);
			if (!string.IsNullOrWhiteSpace(userId))
			{
				var tweets = await _twtclient.GetTweetsByUserId(userId);
				if(tweets != null && tweets.Data.Any())
				{
					var indexed = await _elasticClient.IndexTweets(tweets.Data, "search-tweets");
					return indexed;
				}
			}
			return false;
		}

		public async Task<bool> IndexProducts()
		{
			var products = new Shoes();
			using (StreamReader r = new StreamReader("C:\\Users\\rajat\\source\\repos\\ElasticSearch_DotNet_Demo\\ElasticSearch_DotNet_Demo\\Dataset\\amazon_uk_shoes_dataset.json"))
			{
				string json = r.ReadToEnd();
				products = JsonSerializer.Deserialize<Shoes>(json);
			}

			if (products is not null && products.shoes.Any())
				return await _elasticClient.IndexShoes(products.shoes.ToList(), "search-products");

			return false;
		}

		public async Task<List<Shoe>> FuzzySearchProducts(string query)
		{
			var shoes = await _elasticClient.SearchProducts(query, "search-products");
			return shoes;
		}


			public async Task<List<Tweet>> SearchTweets(string query)
		{
			var tweets = await _elasticClient.SearchDocuments(query, "search-tweets");
			return tweets;
		}
	}
}
