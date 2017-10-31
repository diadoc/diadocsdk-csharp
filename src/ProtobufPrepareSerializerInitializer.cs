using ProtoBuf;

namespace Diadoc.Api
{
	public class ProtobufPrepareSerializerInitializer<T> where T : class
	{
		static ProtobufPrepareSerializerInitializer()
		{
			Serializer.PrepareSerializer<T>();
		}

		public static void Prepare()
		{

		}
	}
}