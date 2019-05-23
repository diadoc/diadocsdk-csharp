using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using JetBrains.Annotations;

namespace Diadoc.Api
{
	public static class XmlSerializerExtensions
	{
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
	}
}