using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Transport;
using ElasticSearch_DotNet_Demo.Models;

namespace ElasticSearch_DotNet_Demo.ElasticSearch
{
	public class ElasticClient
	{
		IConfiguration _config;
		ElasticsearchClient _client;
		private readonly string elastic_cloudId;
		private readonly string elastic_apiKey;
		public ElasticClient(IConfiguration config)
		{
			_config = config;
			elastic_cloudId = _config.GetValue<string>("Elastic:CloudID");
			elastic_apiKey = _config.GetValue<string>("Elastic:ApiKey");
			_client = new ElasticsearchClient(elastic_cloudId, new ApiKey(elastic_apiKey));
		}

		public async Task<bool> IndexDocument(List<Tweet> documentToIndex, string indexName)
		{
			var indexManyResponse = await _client.IndexManyAsync(documentToIndex, indexName);
			return indexManyResponse.Errors;
		}

		internal async Task<List<Tweet>> SearchDocuments(string query, string indexName)
		{
			var response = await _client.SearchAsync<Tweet>(s => s
								.Index(indexName)
								.From(0)
								.Size(10)
								.Query(q => q.Fuzzy(c => c.QueryName("named_query")
								.Boost(2)  //more weight to specific search result, not to be used always
								.Field(p => p.Text)
								.Value(query)
								.MaxExpansions(100)  // the number of variations of the search query to be used
								.PrefixLength(3)  // the length of the characters that should exactly match post which the term query can perform fuzzy search
								.Transpositions(false)  //specify whether or not to allow transpositions (i.e. swapping of adjacent characters) in the matching process
							)
								)
								);

		/*
			 Fuzziness = >control the level of similarity required for a match. Here are a few examples:
			 Auto: The "fuzziness" can be set to "AUTO" which calculates the fuzziness based on the length of the searched term. This is the default value.
			 Edit Distance: The "fuzziness" can be set to a specific edit distance value, where 0 and 1 are considered to be exact matches. For example, a value of 2 allows for up to 2 single character edits (insertions, deletions, or substitutions) to be made to the searched term.
             Proximity: The "fuzziness" can also be set as a value in the range of 0.0 to 1.0, where 0.0 requires an exact match and 1.0 allows for any match. This value is interpreted as a "proximity" value and represents the maximum allowed distance between terms.
             FuzzyQuery: The "fuzziness" can be set to one of the predefined values (e.g. Fuzziness.Auto, Fuzziness.EditDistance(2) )
		 */

			if (response.IsValidResponse)
			{
				var tweet = response.Documents.FirstOrDefault();
			}
			return null;
		}
	}
}
