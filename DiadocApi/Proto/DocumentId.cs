using System;
using System.Runtime.InteropServices;

namespace Diadoc.Api.Proto
{
	[ComVisible(true)]
	[Guid("AADC07B4-DF73-47F1-93EA-6A4C5EEF8D3A")]
	public interface IDocumentId
	{
		string MessageId { get; set; }
		string EntityId { get; set; }
	}

	[ComVisible(true)]
	[Guid("982AC350-BE1B-4234-9CE3-1FF7DF5ACD73")]
	[ProgId("Diadoc.Api.DocumentId")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(IDocumentId))]
	public partial class DocumentId : SafeComObject, IDocumentId, IEquatable<DocumentId>
	{
		public DocumentId(string messageId, string entityId)
		{
			MessageId = messageId;
			EntityId = entityId;
		}

		public override string ToString()
		{
			return string.Format("MessageId: {0}, EntityId: {1}", MessageId, EntityId);
		}

		public bool Equals(DocumentId other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(MessageId, other.MessageId) && string.Equals(EntityId, other.EntityId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((DocumentId) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((MessageId != null ? MessageId.GetHashCode() : 0)*397) ^ (EntityId != null ? EntityId.GetHashCode() : 0);
			}
		}
	}
}
