using JetBrains.Annotations;

namespace Diadoc.Api
{
	public interface IHttpSerializer
	{
		[NotNull]
		byte[] Serialize<T>([NotNull] T obj) where T: class;

		[NotNull]
		T Deserialize<T>([NotNull] byte[] bytes) where T: class;

		[CanBeNull]
		string RequestContentType { get; }

		[CanBeNull]
		string ResponseContentType { get; }
	}
}