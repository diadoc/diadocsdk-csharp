using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Diadoc.Api.Proto;
using Newtonsoft.Json;
using NUnit.Framework;
using ProtoBuf;
using Serializer = ProtoBuf.Serializer;

namespace Diadoc.Api.Tests
{
	[TestFixture]
	public class XmlSerialization_Test
	{
		[TestCaseSource(nameof(GetAllProtoClasses))]
		public void CheckClassesCanBeSerializedToXml(Type protoClass)
		{
			var info = CreateType(protoClass);
			AssertClassCanBeSerializedToXml(info);
		}

		[TestCaseSource(nameof(GetAllProtoClasses))]
		public void CheckClassesCanBeSerializedToJson(Type protoClass)
		{
			var info = CreateType(protoClass);
			AssertClassCanBeSerializedToJson(info);
		}

		[TestCaseSource(nameof(GetAllProtoClasses))]
		public void CheckClassesCanBeSerializedToProtobuf(Type protoClass)
		{
			var info = CreateType(protoClass);
			AssertClassCanBeSerializedToProtobuf(info);
		}

		private static object CreateType(Type protoClass)
		{
			var instance = Activator.CreateInstance(protoClass, BindingFlags.Default, null, null, null);
			FillEnumProperties(instance);
			FillTicksProperties(instance);
			return instance;
		}

		private static void FillEnumProperties(object instance)
		{
			var properties = instance.GetType()
				.GetProperties()
				.Where(x => x.PropertyType.IsEnum && x.CanWrite)
				.ToArray();
			foreach (var property in properties)
			{
				var values = Enum.GetValues(property.PropertyType);
				var defaultValue = values.GetValue(values.Length / 2);
				property.SetValue(instance, defaultValue, null);
			}
		}

		private static void FillTicksProperties(object instance)
		{
			var ticksProperties = instance.GetType()
				.GetProperties()
				.Where(x => x.PropertyType.IsPrimitive && x.Name.EndsWith("Ticks", true, CultureInfo.InvariantCulture))
				.ToArray();
			var ticks = DateTime.Now.Ticks;
			foreach (var property in ticksProperties)
			{
				property.SetValue(instance, ticks, null);
			}

			var timestampProperties = instance.GetType()
				.GetProperties()
				.Where(x => typeof(Timestamp).IsAssignableFrom(x.PropertyType))
				.ToArray();
			foreach (var property in timestampProperties)
			{
				property.SetValue(instance, new Timestamp(DateTime.Now.Ticks), null);
			}
		}

		private static IEnumerable<Type> GetAllProtoClasses()
		{
			var result = typeof(IDiadocApi).Assembly.GetExportedTypes()
				.Where(x => x.IsClass && typeof(IExtensible).IsAssignableFrom(x));
			return result;
		}

		private static void AssertClassCanBeSerializedToXml(object info)
		{
			var serializer = GetSerializer(info);
			var memoryStream = new MemoryStream();
			serializer.Serialize(memoryStream, info);
		}

		private static void AssertClassCanBeSerializedToJson(object info)
		{
			var serializer = new JsonSerializer();
			var textWriter = new StringWriter();
			serializer.Serialize(textWriter, info);
		}

		private static void AssertClassCanBeSerializedToProtobuf(object info)
		{
			var memoryStream = new MemoryStream();
			Serializer.NonGeneric.Serialize(memoryStream, info);
		}

		private static XmlSerializer GetSerializer(object info)
		{
			return new XmlSerializer(info.GetType());
		}
	}
}