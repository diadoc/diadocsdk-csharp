using System;
using Diadoc.Api.Constants;
using Diadoc.Api.Http;
using Diadoc.Api.Proto.Invoicing;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public InvoiceInfo ParseInvoiceXml(byte[] invoiceXmlContent)
		{
			return ParseInvoiceXml(null, invoiceXmlContent);
		}

		public InvoiceInfo ParseInvoiceXml(string authToken, byte[] invoiceXmlContent)
		{
			return PerformHttpRequest<InvoiceInfo>(authToken, "POST", "/ParseInvoiceXml", invoiceXmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Torg12SellerTitleInfo ParseTorg12SellerTitleXml(byte[] xmlContent)
		{
			return ParseTorg12SellerTitleXml(null, xmlContent);
		}

		public Torg12SellerTitleInfo ParseTorg12SellerTitleXml(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequest<Torg12SellerTitleInfo>(authToken, "POST", "/ParseTorg12SellerTitleXml", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public Torg12BuyerTitleInfo ParseTorg12BuyerTitleXml(byte[] xmlContent)
		{
			return ParseTorg12BuyerTitleXml(null, xmlContent);
		}

		public Torg12BuyerTitleInfo ParseTorg12BuyerTitleXml(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequest<Torg12BuyerTitleInfo>(authToken, "POST", "/ParseTorg12BuyerTitleXml", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public TovTorgSellerTitleInfo ParseTovTorg551SellerTitleXml(byte[] xmlContent)
		{
			return ParseTovTorg551SellerTitleXml(null, xmlContent);
		}

		public TovTorgSellerTitleInfo ParseTovTorg551SellerTitleXml(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequest<TovTorgSellerTitleInfo>(authToken, "POST", $"/ParseTorg12SellerTitleXml?documentVersion={DefaultDocumentVersions.TovTorg551}", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public TovTorgBuyerTitleInfo ParseTovTorg551BuyerTitleXml(byte[] xmlContent)
		{
			return ParseTovTorg551BuyerTitleXml(null, xmlContent);
		}

		public TovTorgBuyerTitleInfo ParseTovTorg551BuyerTitleXml(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequest<TovTorgBuyerTitleInfo>(authToken, "POST", $"/ParseTorg12BuyerTitleXml?documentVersion={DefaultDocumentVersions.TovTorg551}", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public AcceptanceCertificateSellerTitleInfo ParseAcceptanceCertificateSellerTitleXml(byte[] xmlContent)
		{
			return ParseAcceptanceCertificateSellerTitleXml(null, xmlContent);
		}

		public AcceptanceCertificateSellerTitleInfo ParseAcceptanceCertificateSellerTitleXml(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequest<AcceptanceCertificateSellerTitleInfo>(authToken, "POST", "/ParseAcceptanceCertificateSellerTitleXml", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public AcceptanceCertificateBuyerTitleInfo ParseAcceptanceCertificateBuyerTitleXml(byte[] xmlContent)
		{
			return ParseAcceptanceCertificateBuyerTitleXml(null, xmlContent);
		}

		public AcceptanceCertificateBuyerTitleInfo ParseAcceptanceCertificateBuyerTitleXml(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequest<AcceptanceCertificateBuyerTitleInfo>(authToken, "POST", "/ParseAcceptanceCertificateBuyerTitleXml", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public AcceptanceCertificate552SellerTitleInfo ParseAcceptanceCertificate552SellerTitleXml(byte[] xmlContent)
		{
			return ParseAcceptanceCertificate552SellerTitleXml(null, xmlContent);
		}

		public AcceptanceCertificate552SellerTitleInfo ParseAcceptanceCertificate552SellerTitleXml(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequest<AcceptanceCertificate552SellerTitleInfo>(authToken, "POST", $"/ParseAcceptanceCertificateSellerTitleXml?documentVersion={DefaultDocumentVersions.AcceptanceCerttificate552}", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public AcceptanceCertificate552BuyerTitleInfo ParseAcceptanceCertificate552BuyerTitleXml(byte[] xmlContent)
		{
			return ParseAcceptanceCertificate552BuyerTitleXml(null, xmlContent);
		}

		public AcceptanceCertificate552BuyerTitleInfo ParseAcceptanceCertificate552BuyerTitleXml(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequest<AcceptanceCertificate552BuyerTitleInfo>(authToken, "POST", $"/ParseAcceptanceCertificateBuyerTitleXml?documentVersion={DefaultDocumentVersions.AcceptanceCerttificate552}", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public UniversalTransferDocumentSellerTitleInfo ParseUniversalTransferDocumentSellerTitleXml(byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Utd)
		{
			return ParseUniversalTransferDocumentSellerTitleXml(null, xmlContent, documentVersion);
		}

		public UniversalTransferDocumentSellerTitleInfo ParseUniversalTransferDocumentSellerTitleXml(string authToken, byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Utd)
		{
			var query = new PathAndQueryBuilder("/ParseUniversalTransferDocumentSellerTitleXml");
			query.AddParameter("documentVersion", documentVersion);
			return PerformHttpRequest<UniversalTransferDocumentSellerTitleInfo>(authToken, "POST", query.BuildPathAndQuery(), xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public UniversalTransferDocumentBuyerTitleInfo ParseUniversalTransferDocumentBuyerTitleXml(byte[] xmlContent)
		{
			return ParseUniversalTransferDocumentBuyerTitleXml(null, xmlContent);
		}

		public UniversalTransferDocumentBuyerTitleInfo ParseUniversalTransferDocumentBuyerTitleXml(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequest<UniversalTransferDocumentBuyerTitleInfo>(authToken, "POST", "/ParseUniversalTransferDocumentBuyerTitleXml", xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public UniversalCorrectionDocumentSellerTitleInfo ParseUniversalCorrectionDocumentSellerTitleXml(byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Ucd)
		{
			return ParseUniversalCorrectionDocumentSellerTitleXml(null, xmlContent, documentVersion);
		}

		public UniversalCorrectionDocumentSellerTitleInfo ParseUniversalCorrectionDocumentSellerTitleXml(string authToken, byte[] xmlContent, string documentVersion = DefaultDocumentVersions.Ucd)
		{
			var query = new PathAndQueryBuilder("/ParseUniversalCorrectionDocumentSellerTitleXml");
			query.AddParameter("documentVersion", documentVersion);
			return PerformHttpRequest<UniversalCorrectionDocumentSellerTitleInfo>(authToken, "POST", query.BuildPathAndQuery(), xmlContent);
		}

		[Obsolete(ObsoleteReasons.UseAuthTokenOverload)]
		public UniversalTransferDocumentBuyerTitleInfo ParseUniversalCorrectionDocumentBuyerTitleXml(byte[] xmlContent)
		{
			return ParseUniversalCorrectionDocumentBuyerTitleXml(null, xmlContent);
		}

		public UniversalTransferDocumentBuyerTitleInfo ParseUniversalCorrectionDocumentBuyerTitleXml(string authToken, byte[] xmlContent)
		{
			return PerformHttpRequest<UniversalTransferDocumentBuyerTitleInfo>(authToken, "POST", "/ParseUniversalCorrectionDocumentBuyerTitleXml", xmlContent);
		}

		public byte[] ParseTitleXml(
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

			return PerformHttpRequest(authToken, "POST", queryBuilder.BuildPathAndQuery(), content);
		}
	}
}
