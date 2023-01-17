using ElasticSearch_DotNet_Demo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch_DotNet_Demo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SearchIndexController : ControllerBase
	{
		ISearchIndexer _indexer;
		public SearchIndexController(ISearchIndexer indexer)
		{
			_indexer = indexer;
		}

		[HttpPost]
		[Route("/tweets/{userName}")]
		public async Task<IActionResult> IndexTweetsByUser(string userName)
		{
			var response = await _indexer.IndexTweetsByUserName(userName);
			return Ok(response);
		}

		[HttpPost]
		[Route("/tweets/search")]
		public async Task<IActionResult> SearchTweets(string query)
		{
			var response = await _indexer.SearchTweets(query);
			return Ok(response);
		}
	}
}
