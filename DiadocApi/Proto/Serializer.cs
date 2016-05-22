using System.IO;

namespace Diadoc.Api.Proto
{
	public static class Serializer
	{
		public static T Deserialize<T>(Stream source)
		{
			return ProtoBuf.Serializer.Deserialize<T>(source);
		}

		public static void Serialize<T>(Stream destination, T instance)
		{
			ProtoBuf.Serializer.Serialize(destination, instance);
		}
	}
}