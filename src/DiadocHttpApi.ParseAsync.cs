using System;
using System.Threading.Tasks;
using Diadoc.Api.Constants;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Invoicing;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<InvoiceInfo> ParseInvoiceXmlAsync(byte[] invoiceXmlContent)
		{
			return ParseInvoiceXmlAsync(null, invoiceXmlContent);
		}

		public Task<InvoiceInfo> ParseInvoiceXmlAsync(string authToken, byte[] invoiceXmlContent)
		{
			return PerformHttpRequestAsync<InvoiceInfo>(authToken, "POST", "/ParseInvoiceXml", invoiceXmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<Torg12SellerTitleInfo> ParseTorg12SellerTitleXmlAsync(byte[] xmlContent)
		{
			return ParseTorg12SellerTitleXmlAsync(null, xmlContent);
		}

		public Task<Torg12SellerTitleInfo> ParseTorg12SellerTitleXmlAsync(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequestAsync<Torg12SellerTitleInfo>(authToken, "POST", "/ParseTorg12SellerTitleXml", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<Torg12BuyerTitleInfo> ParseTorg12BuyerTitleXmlAsync(byte[] xmlContent)
		{
			return ParseTorg12BuyerTitleXmlAsync(null, xmlContent);
		}

		public Task<Torg12BuyerTitleInfo> ParseTorg12BuyerTitleXmlAsync(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequestAsync<Torg12BuyerTitleInfo>(authToken, "POST", "/ParseTorg12BuyerTitleXml", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<TovTorgSellerTitleInfo> ParseTovTorg551SellerTitleXmlAsync(byte[] xmlContent)
		{
			return ParseTovTorg551SellerTitleXmlAsync(null, xmlContent);
		}

		public Task<TovTorgSellerTitleInfo> ParseTovTorg551SellerTitleXmlAsync(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequestAsync<TovTorgSellerTitleInfo>(authToken, "POST", $"/ParseTorg12SellerTitleXml?documentVersion={DefaultDocumentVersions.TovTorg551}", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<TovTorgBuyerTitleInfo> ParseTovTorg551BuyerTitleXmlAsync(byte[] xmlContent)
		{
			return ParseTovTorg551BuyerTitleXmlAsync(null, xmlContent);
		}

		public Task<TovTorgBuyerTitleInfo> ParseTovTorg551BuyerTitleXmlAsync(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequestAsync<TovTorgBuyerTitleInfo>(authToken, "POST", $"/ParseTorg12BuyerTitleXml?documentVersion={DefaultDocumentVersions.TovTorg551}", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<AcceptanceCertificateSellerTitleInfo> ParseAcceptanceCertificateSellerTitleXmlAsync(byte[] xmlContent)
		{
			return ParseAcceptanceCertificateSellerTitleXmlAsync(null, xmlContent);
		}

		public Task<AcceptanceCertificateSellerTitleInfo> ParseAcceptanceCertificateSellerTitleXmlAsync(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequestAsync<AcceptanceCertificateSellerTitleInfo>(authToken, "POST", "/ParseAcceptanceCertificateSellerTitleXml", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<AcceptanceCertificateBuyerTitleInfo> ParseAcceptanceCertificateBuyerTitleXmlAsync(byte[] xmlContent)
		{
			return ParseAcceptanceCertificateBuyerTitleXmlAsync(null, xmlContent);
		}

		public Task<AcceptanceCertificateBuyerTitleInfo> ParseAcceptanceCertificateBuyerTitleXmlAsync(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequestAsync<AcceptanceCertificateBuyerTitleInfo>(authToken, "POST", "/ParseAcceptanceCertificateBuyerTitleXml", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<AcceptanceCertificate552SellerTitleInfo> ParseAcceptanceCertificate552SellerTitleXmlAsync(byte[] xmlContent)
		{
			return ParseAcceptanceCertificate552SellerTitleXmlAsync(null, xmlContent);
		}

		public Task<AcceptanceCertificate552SellerTitleInfo> ParseAcceptanceCertificate552SellerTitleXmlAsync(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequestAsync<AcceptanceCertificate552SellerTitleInfo>(authToken, "POST", $"/ParseAcceptanceCertificateSellerTitleXml?documentVersion={DefaultDocumentVersions.AcceptanceCerttificate552}", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<AcceptanceCertificate552BuyerTitleInfo> ParseAcceptanceCertificate552BuyerTitleXmlAsync(byte[] xmlContent)
		{
			return ParseAcceptanceCertificate552BuyerTitleXmlAsync(null, xmlContent);
		}

		public Task<AcceptanceCertificate552BuyerTitleInfo> ParseAcceptanceCertificate552BuyerTitleXmlAsync(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequestAsync<AcceptanceCertificate552BuyerTitleInfo>(authToken, "POST", $"/ParseAcceptanceCertificateBuyerTitleXml?documentVersion={DefaultDocumentVersions.AcceptanceCerttificate552}", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<UniversalTransferDocumentSellerTitleInfo> ParseUniversalTransferDocumentSellerTitleXmlAsync(byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Utd)
		{
			return ParseUniversalTransferDocumentSellerTitleXmlAsync(null, xmlContent, documentVersion);
		}

		public Task<UniversalTransferDocumentSellerTitleInfo> ParseUniversalTransferDocumentSellerTitleXmlAsync(string authToken, byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Utd)
		{
			var query = new PathAndQueryBuilder("/ParseUniversalTransferDocumentSellerTitleXml");
			query.AddParameter("documentVersion", documentVersion);
			return PerformHttpRequestAsync<UniversalTransferDocumentSellerTitleInfo>(authToken, "POST", query.BuildPathAndQuery(), xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalTransferDocumentBuyerTitleXmlAsync(byte[] xmlContent)
		{
			return ParseUniversalTransferDocumentBuyerTitleXmlAsync(null, xmlContent);
		}

		public Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalTransferDocumentBuyerTitleXmlAsync(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequestAsync<UniversalTransferDocumentBuyerTitleInfo>(authToken, "POST", "/ParseUniversalTransferDocumentBuyerTitleXml", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<UniversalCorrectionDocumentSellerTitleInfo> ParseUniversalCorrectionDocumentSellerTitleXmlAsync(byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Ucd)
		{
			return ParseUniversalCorrectionDocumentSellerTitleXmlAsync(null, xmlContent, documentVersion);
		}

		public Task<UniversalCorrectionDocumentSellerTitleInfo> ParseUniversalCorrectionDocumentSellerTitleXmlAsync(string authToken, byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Ucd)
		{
			var query = new PathAndQueryBuilder("/ParseUniversalCorrectionDocumentSellerTitleXml");
			query.AddParameter("documentVersion", documentVersion);
			return PerformHttpRequestAsync<UniversalCorrectionDocumentSellerTitleInfo>(authToken, "POST", query.BuildPathAndQuery(), xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalCorrectionDocumentBuyerTitleXmlAsync(byte[] xmlContent)
		{
			return ParseUniversalCorrectionDocumentBuyerTitleXmlAsync(null, xmlContent);
		}

		public Task<UniversalTransferDocumentBuyerTitleInfo> ParseUniversalCorrectionDocumentBuyerTitleXmlAsync(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequestAsync<UniversalTransferDocumentBuyerTitleInfo>(authToken, "POST", "/ParseUniversalCorrectionDocumentBuyerTitleXml", xmlContent);
		}

		public Task<byte[]> ParseTitleXmlAsync(
			string authToken,
			string boxId,
			string documentTypeNamedId,
			string documentFunction,
			string documentVersion,
			int titleIndex,
			byte[] content)
		{
			var queryBuilder = new PathAndQueryBuilder("/ParseTitleXml");
			queryBuilder.AddParameter("boxId", boxId);
			queryBuilder.AddParameter("documentTypeNamedId", documentTypeNamedId);
			queryBuilder.AddParameter("documentFunction", documentFunction);
			queryBuilder.AddParameter("documentVersion", documentVersion);
			queryBuilder.AddParameter("titleIndex", titleIndex.ToString());

			return PerformHttpRequestAsync(authToken, "POST", queryBuilder.BuildPathAndQuery(), content);
		}
	}
}
