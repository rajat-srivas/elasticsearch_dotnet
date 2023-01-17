using ElasticSearch_DotNet_Demo.Models;

namespace ElasticSearch_DotNet_Demo.Repository
{
	public interface ISearchIndexer
	{
		Task<bool> IndexTweetsByUserName(string userName);

		Task<List<Tweet>> SearchTweets(string query);
	}
}
