using ElasticSearch_DotNet_Demo.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json;

namespace ElasticSearch_DotNet_Demo.Util
{
	public class TwitterClient
	{
		IConfiguration _config;
		private readonly string auth_token = "";
		public TwitterClient(IConfiguration config)
		{
			_config = config;
			auth_token = _config.GetValue<string>("TwitterToken");

		}

		public async Task<TweetData> GetTweetsByUserId(string userId)
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"https://api.twitter.com/2/users/{userId}/tweets?max_results=100"),
				Headers =
						{
							{ "Authorization", $"bearer {auth_token}" },
						},
			};
			using (var response = await client.SendAsync(request))
			{
				var tweets = new TweetData();
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				if (!string.IsNullOrEmpty(body))
				{
					tweets = JsonConvert.DeserializeObject<TweetData>(body);
				}

				return tweets ;
			}
		}

		public async Task<string> GetUserIdByUserName(string userName)
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"https://api.twitter.com/2/users/by/username/{userName}"),
				Headers =
						{
							{ "Authorization", $"bearer {auth_token}" },
						},
			};
			using (var response = await client.SendAsync(request))
			{
				var user = new UserData();
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				if (!string.IsNullOrEmpty(body))
				{
					user = JsonConvert.DeserializeObject<UserData>(body);
				}

				return user?.Data?.Id;
			}
		}
	}
}
