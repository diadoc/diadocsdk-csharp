using System.Collections.Generic;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.KeyValueStorage;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[NotNull]
		public List<KeyValueStorageEntry> GetOrganizationStorageEntries(
			[NotNull] string authToken,
			[NotNull] string boxId,
			[NotNull] IEnumerable<string> keys)
		{
			var qsb = new PathAndQueryBuilder("/V2/KeyValueStorageGet");
			qsb.AddParameter("boxId", boxId);
			var queryString = qsb.BuildPathAndQuery();

			var request = new KeyValueStorageApiGetRequest();
			request.Keys.AddRange(keys);

			var response =
				PerformHttpRequest<KeyValueStorageApiGetRequest, KeyValueStorageApiGetResponse>(
					authToken,
					queryString,
					request);
			return response.Entries;
		}

		public void PutOrganizationStorageEntries(
			[NotNull] string authToken,
			[NotNull] string boxId,
			[NotNull] IEnumerable<KeyValueStorageEntry> entries)
		{
			var qsb = new PathAndQueryBuilder("/V2/KeyValueStoragePut");
			qsb.AddParameter("boxId", boxId);
			var queryString = qsb.BuildPathAndQuery();

			var request = new KeyValueStorageApiPutRequest();
			request.Entries.AddRange(entries);

			PerformHttpRequest(authToken, "POST", queryString, Serialize(request));
		}
	}
}