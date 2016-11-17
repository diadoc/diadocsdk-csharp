using Diadoc.Api.Proto.Forwarding;
using JetBrains.Annotations;

namespace Diadoc.Api.Http
{
	public static class PathAndQueryBuilderExtensions
	{
		[NotNull]
		public static PathAndQueryBuilder With([NotNull] this PathAndQueryBuilder qsb, [NotNull] string paramName, [CanBeNull] string paramValue)
		{
			qsb.AddParameter(paramName, paramValue);
			return qsb;
		}

		[NotNull]
		public static PathAndQueryBuilder WithBoxId([NotNull] this PathAndQueryBuilder qsb, [NotNull] string boxId)
		{
			qsb.AddParameter("boxId", boxId);
			return qsb;
		}

		[NotNull]
		public static PathAndQueryBuilder WithForwardedDocumentId([NotNull] this PathAndQueryBuilder qsb, [NotNull] ForwardedDocumentId forwardedDocumentId)
		{
			qsb.AddParameter("fromBoxId", forwardedDocumentId.FromBoxId);
			qsb.AddParameter("messageId", forwardedDocumentId.DocumentId.MessageId);
			qsb.AddParameter("documentId", forwardedDocumentId.DocumentId.EntityId);
			qsb.AddParameter("forwardEventId", forwardedDocumentId.ForwardEventId);
			return qsb;
		}
	}
}