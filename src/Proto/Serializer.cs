using System.IO;
using Diadoc.Api.Proto.Docflow;

namespace Diadoc.Api.Proto
{
	public static class Serializer
	{
		static Serializer()
		{
			ProtoBuf.Serializer.PrepareSerializer<Organization>();
			ProtoBuf.Serializer.PrepareSerializer<Box>();
			ProtoBuf.Serializer.PrepareSerializer<ConfirmationDocflow>();
			ProtoBuf.Serializer.PrepareSerializer<ReceiptDocflowV3>();
		}

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
