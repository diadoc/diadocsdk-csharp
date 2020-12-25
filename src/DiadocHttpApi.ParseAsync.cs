using System.Threading;
using System.Threading.Tasks;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Invoicing;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public Task<InvoiceInfo> ParseInvoiceXmlAsync(byte[] invoiceXmlContent, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<InvoiceInfo>(null, "POST", "/ParseInvoiceXml", invoiceXmlContent, ct: ct);
		}

		public Task<Torg12SellerTitleInfo> ParseTorg12SellerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<Torg12SellerTitleInfo>(null, "POST", "/ParseTorg12SellerTitleXml", xmlContent, ct: ct);
		}

		public Task<Torg12BuyerTitleInfo> ParseTorg12BuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<Torg12BuyerTitleInfo>(null, "POST", "/ParseTorg12BuyerTitleXml", xmlContent, ct: ct);
		}

		public Task<TovTorgSellerTitleInfo> ParseTovTorg551SellerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<TovTorgSellerTitleInfo>(null, "POST", $"/ParseTorg12SellerTitleXml?documentVersion={DefaultDocumentVersions.TovTorg551}", xmlContent, ct: ct);
		}

		public Task<TovTorgBuyerTitleInfo> ParseTovTorg551BuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<TovTorgBuyerTitleInfo>(null, "POST", $"/ParseTorg12BuyerTitleXml?documentVersion={DefaultDocumentVersions.TovTorg551}", xmlContent, ct: ct);
		}

		public Task<AcceptanceCertificateSellerTitleInfo> ParseAcceptanceCertificateSellerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<AcceptanceCertificateSellerTitleInfo>(null, "POST", "/ParseAcceptanceCertificateSellerTitleXml", xmlContent, ct: ct);
		}

		public Task<AcceptanceCertificateBuyerTitleInfo> ParseAcceptanceCertificateBuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<AcceptanceCertificateBuyerTitleInfo>(null, "POST", "/ParseAcceptanceCertificateBuyerTitleXml", xmlContent, ct: ct);
		}

		public Task<AcceptanceCertificate552SellerTitleInfo> ParseAcceptanceCertificate552SellerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<AcceptanceCertificate552SellerTitleInfo>(null, "POST", $"/ParseAcceptanceCertificateSellerTitleXml?documentVersion={DefaultDocumentVersions.AcceptanceCerttificate552}", xmlContent, ct: ct);
		}

		public Task<AcceptanceCertificate552BuyerTitleInfo> ParseAcceptanceCertificate552BuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<AcceptanceCertificate552BuyerTitleInfo>(null, "POST", $"/ParseAcceptanceCertificateBuyerTitleXml?documentVersion={DefaultDocumentVersions.AcceptanceCerttificate552}", xmlContent, ct: ct);
		}

		public Task<UniversalTransferDocumentSellerTitleInfo> ParseUniversalTransferDocumentSellerTitleXmlAsync(byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Utd, CancellationToken ct = default)
		{
			var query = new PathAndQueryBuilder("/ParseUniversalTransferDocumentSellerTitleXml");
			query.AddParameter("documentVersion", documentVersion);
			return PerformHttpRequestAsync<UniversalTransferDocumentSellerTitleInfo>(null, "POST", query.BuildPathAndQuery(), xmlContent, ct: ct);
		}

		public Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalTransferDocumentBuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<UniversalTransferDocumentBuyerTitleInfo>(null, "POST", "/ParseUniversalTransferDocumentBuyerTitleXml", xmlContent, ct: ct);
		}

		public Task<UniversalCorrectionDocumentSellerTitleInfo> ParseUniversalCorrectionDocumentSellerTitleXmlAsync(byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Ucd, CancellationToken ct = default)
		{
			var query = new PathAndQueryBuilder("/ParseUniversalCorrectionDocumentSellerTitleXml");
			query.AddParameter("documentVersion", documentVersion);
			return PerformHttpRequestAsync<UniversalCorrectionDocumentSellerTitleInfo>(null, "POST", query.BuildPathAndQuery(), xmlContent, ct: ct);
		}

		public Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalCorrectionDocumentBuyerTitleXmlAsync(byte[] xmlContent, CancellationToken ct = default)
		{
			return PerformHttpRequestAsync<UniversalTransferDocumentBuyerTitleInfo>(null, "POST", "/ParseUniversalCorrectionDocumentBuyerTitleXml", xmlContent, ct: ct);
		}

		public Task<byte[]> ParseTitleXmlAsync(
			string authToken,
			string boxId,
			string documentTypeNamedId,
			string documentFunction,
			string documentVersion,
			int titleIndex,
			byte[] content, 
			CancellationToken ct = default)
		{
			var queryBuilder = new PathAndQueryBuilder("/ParseTitleXml");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("documentTypeNamedId", documentTypeNamedId);
			queryBuilder.AddParameter("documentFunction", documentFunction);
			queryBuilder.AddParameter("documentVersion", documentVersion);
			queryBuilder.AddParameter("titleIndex", titleIndex.ToString());

			return PerformHttpRequestAsync(authToken, "POST", queryBuilder.BuildPathAndQuery(), content, ct: ct);
		}
	}
}