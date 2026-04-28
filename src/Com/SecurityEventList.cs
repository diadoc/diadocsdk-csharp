using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Diadoc.Api.Com;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("521351dc-f200-4c1b-8e27-c0af0db70add")]
	public interface ISecurityEventList
	{
		ReadonlyList EventsList { get; }
		string LastIndexKey { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SecurityEventList")]
	[Guid("c6ce28f5-e220-4f82-add0-7df0919bc0cc")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISecurityEventList))]
	public partial class SecurityEventList : SafeComObject, ISecurityEventList
	{
		public ReadonlyList EventsList => new ReadonlyList(Events);
	}

	[ComVisible(true)]
	[Guid("88d84b27-7c24-4d93-b29a-1f8b34680bf6")]
	public interface ISecurityEvent
	{
		string EventId { get; set; }
		Com.SecurityEventType EventType { get; set; }
		SecurityEventTarget Target { get; set; }
		long TimestampTicks { get; set; }
		string Details { get; set; }
		string IndexKey { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SecurityEvent")]
	[Guid("6f3e5a7b-acf9-4b95-bc37-e0c9905b3ad2")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISecurityEvent))]
	public partial class SecurityEvent : SafeComObject, ISecurityEvent
	{
		Com.SecurityEventType ISecurityEvent.EventType
		{
			get => (Com.SecurityEventType) EventType;
			set => EventType = (SecurityEventType) value;
		}
	}

	[ComVisible(true)]
	[Guid("8090f744-e139-4aea-a94f-90f654be514b")]
	public interface ISecurityEventTarget
	{
		Com.SecurityEventTargetType Type { get; set; }
		DocumentTarget Document { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.SecurityEventTarget")]
	[Guid("2bd65d08-d7ab-488b-bcae-a556ef8c6380")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(ISecurityEventTarget))]
	public partial class SecurityEventTarget : SafeComObject, ISecurityEventTarget
	{
		Com.SecurityEventTargetType ISecurityEventTarget.Type
		{
			get => (Com.SecurityEventTargetType) Type;
			set => Type = (SecurityEventTargetType) value;
		}
	}

	[ComVisible(true)]
	[Guid("5d26d781-4715-4d65-a403-13f99d0e4a32")]
	public interface IDocumentTarget
	{
		string LetterId { get; set; }
		string DocumentId { get; set; }
	}

	[ComVisible(true)]
	[ProgId("Diadoc.Api.DocumentTarget")]
	[Guid("4113342f-6b04-472d-aea8-0add61ecbe95")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentTarget))]
	public partial class DocumentTarget : SafeComObject, IDocumentTarget
	{
	}
}

namespace Diadoc.Api.Com
{
	[ComVisible(true)]
	[Guid("a98eadf9-c777-4efb-b2c6-331ad4fc4186")]
	[XmlType(TypeName = "SecurityEventTargetType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum SecurityEventTargetType
	{
		UnknownTargetType = 0,
		Document = 1
	}

	[ComVisible(true)]
	[Guid("b1d564c1-635d-4a2c-80a8-d405e2fc7920")]
	[XmlType(TypeName = "SecurityEventType", Namespace = "https://diadoc-api.kontur.ru")]
	public enum SecurityEventType
	{
		UnknownEventType = 0,
		MalwareDetected = 1
	}
}
