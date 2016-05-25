using Diadoc.Api.Annotations;

namespace Diadoc.Api
{
	public interface IHttpSerializer
	{
		[NotNull]
		byte[] Serialize<T>([NotNull] T obj);

		[NotNull]
		T Deserialize<T>([NotNull] byte[] bytes);

		[CanBeNull]
		string RequestContentType { get; }

		[CanBeNull]
		string ResponseContentType { get; }
	}
}