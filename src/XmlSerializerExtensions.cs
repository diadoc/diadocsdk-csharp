using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public static class XmlSerializerExtensions
	{
		public static byte[] NullifyEmptyStringPropertiesAndSerializeToXml<T>(this T @object)
		{
			@object.NullifyEmptyStringProperties();
			return SerializeToXml(@object);
		}

		public static byte[] SerializeToXml<T>(this T @object)
		{
			var serializer = new XmlSerializer(typeof(T));
			using (var ms = new MemoryStream())
			{
				using (var sw = new StreamWriter(ms, Encoding.UTF8))
				{
					XmlSerializerNamespaces namespaces = null;
					var ns = FindXmlNamespace<T>();
					if (!IsNullOrWhiteSpace(ns))
					{
						namespaces = new XmlSerializerNamespaces();
						namespaces.Add("", ns);
					}

					serializer.Serialize(sw, @object, namespaces ?? new XmlSerializerNamespaces(new[] {new XmlQualifiedName(string.Empty)}));
				}

				return ms.ToArray();
			}
		}

		[CanBeNull]
		private static string FindXmlNamespace<T>()
		{
			var root = typeof(T).GetCustomAttributes(typeof(XmlRootAttribute), true).Cast<XmlRootAttribute>().FirstOrDefault();
			return root != null && !IsNullOrWhiteSpace(root.Namespace) ? root.Namespace : null;
		}

		private static bool IsNullOrWhiteSpace(string value) => string.IsNullOrEmpty(value) || value.Trim().Length == 0;

		private static void NullifyEmptyStringProperties(this object obj)
		{
			if (obj == null)
				return;
			var propertyInfos = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (var propertyInfo in propertyInfos)
			{
				var value = propertyInfo.GetValue(obj, index: null);
				switch (value)
				{
					case string stringValue:
						if (stringValue == "")
							propertyInfo.SetValue(obj, value: null, index: null);
						break;
					case Array arrayValue:
						foreach (var item in arrayValue)
							NullifyEmptyStringProperties(item);
						break;
					default:
						NullifyEmptyStringProperties(value);
						break;
				}
			}
		}
	}
}