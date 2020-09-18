using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.KeyValueStorage;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[ItemNotNull]
		public async Task<List<KeyValueStorageEntry>> GetOrganizationStorageEntriesAsync(
			[NotNull] string authToken,
			[NotNull] string boxId,
			[NotNull] IEnumerable<string> keys, 
			CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/V2/KeyValueStorageGet");
			qsb.AddParameter("boxId", boxId);
			var queryString = qsb.BuildPathAndQuery();

			var request = new KeyValueStorageApiGetRequest();
			request.Keys.AddRange(keys);

			var response = await PerformHttpRequestAsync<KeyValueStorageApiGetRequest, KeyValueStorageApiGetResponse>(
					authToken,
					queryString,
					request,
					ct: ct)
				.ConfigureAwait(false);
			return response.Entries;
		}

		public Task PutOrganizationStorageEntriesAsync(
			[NotNull] string authToken,
			[NotNull] string boxId,
			[NotNull] IEnumerable<KeyValueStorageEntry> entries, 
			CancellationToken ct = default)
		{
			var qsb = new PathAndQueryBuilder("/V2/KeyValueStoragePut");
			qsb.AddParameter("boxId", boxId);
			var queryString = qsb.BuildPathAndQuery();

			var request = new KeyValueStorageApiPutRequest();
			request.Entries.AddRange(entries);

			return PerformHttpRequestAsync(authToken, "POST", queryString, Serialize(request), ct: ct);
		}
	}
}