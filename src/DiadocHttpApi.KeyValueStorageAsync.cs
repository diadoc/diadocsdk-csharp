using System.Collections.Generic;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.KeyValueStorage;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[ItemNotNull]
		public async Task<List<KeyValueStorageEntry>> GetOrganizationStorageEntriesAsync([NotNull] string authToken, [NotNull] string orgId, [NotNull] IEnumerable<string> keys)
		{
			var qsb = new PathAndQueryBuilder("/KeyValueStorageGet");
			qsb.AddParameter("orgId", orgId);
			var queryString = qsb.BuildPathAndQuery();

			var request = new KeyValueStorageApiGetRequest();
			request.Keys.AddRange(keys);

			var response = await PerformHttpRequestAsync<KeyValueStorageApiGetRequest, KeyValueStorageApiGetResponse>(authToken, queryString, request).ConfigureAwait(false);
			return response.Entries;
		}

		public Task PutOrganizationStorageEntriesAsync([NotNull] string authToken, [NotNull] string orgId, [NotNull] IEnumerable<KeyValueStorageEntry> entries)
		{
			var qsb = new PathAndQueryBuilder("/KeyValueStoragePut");
			qsb.AddParameter("orgId", orgId);
			var queryString = qsb.BuildPathAndQuery();

			var request = new KeyValueStorageApiPutRequest();
			request.Entries.AddRange(entries);

			return PerformHttpRequestAsync(authToken, "POST", queryString, Serialize(request));
		}
	}
}