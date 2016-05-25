using System.IO;
using Diadoc.Api.Annotations;
using ProtoBuf;

namespace Diadoc.Api
{
	public class ProtobufHttpSerializer: IHttpSerializer
	{
		[NotNull]
		public byte[] Serialize<T>([NotNull] T obj)
		{
			using (var mem = new MemoryStream())
			{
				Serializer.Serialize(mem, obj);
				return mem.ToArray();
			}
		}

		[NotNull]
		public T Deserialize<T>([NotNull] byte[] bytes)
		{
			using (var ms = new MemoryStream(bytes))
				return Serializer.Deserialize<T>(ms);
		}

		[CanBeNull]
		public string RequestContentType
		{
			get { return null; }
		}

		[CanBeNull]
		public string ResponseContentType
		{
			get { return null; }
		}
	}
}