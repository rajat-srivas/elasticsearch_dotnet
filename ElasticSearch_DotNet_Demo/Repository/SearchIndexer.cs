using ElasticSearch_DotNet_Demo.ElasticSearch;
using ElasticSearch_DotNet_Demo.Models;
using ElasticSearch_DotNet_Demo.Util;

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
					var indexed = await _elasticClient.IndexDocument(tweets.Data, "search-tweets");
					return indexed;
				}
			}
			return false;
		}

		public async Task<List<Tweet>> SearchTweets(string query)
		{
			var tweets = await _elasticClient.SearchDocuments(query, "search-tweets");
			return tweets;
		}
	}
}
