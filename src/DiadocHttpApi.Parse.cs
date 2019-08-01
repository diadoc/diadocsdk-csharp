using Diadoc.Api.Http;
using Diadoc.Api.Proto.Invoicing;

namespace Diadoc.Api
{
	public partial class DiadocHttpApi
	{
		public InvoiceInfo ParseInvoiceXml(byte[] invoiceXmlContent)
		{
			return PerformHttpRequest<InvoiceInfo>(null, "POST", "/ParseInvoiceXml", invoiceXmlContent);
		}

		public Torg12SellerTitleInfo ParseTorg12SellerTitleXml(byte[] xmlContent)
		{
			return PerformHttpRequest<Torg12SellerTitleInfo>(null, "POST", "/ParseTorg12SellerTitleXml", xmlContent);
		}

		public Torg12BuyerTitleInfo ParseTorg12BuyerTitleXml(byte[] xmlContent)
		{
			return PerformHttpRequest<Torg12BuyerTitleInfo>(null, "POST", "/ParseTorg12BuyerTitleXml", xmlContent);
		}

		public TovTorgSellerTitleInfo ParseTovTorg551SellerTitleXml(byte[] xmlContent)
		{
			return PerformHttpRequest<TovTorgSellerTitleInfo>(null, "POST", $"/ParseTorg12SellerTitleXml?documentVersion={DefaultDocumentVersions.TovTorg551}", xmlContent);
		}

		public TovTorgBuyerTitleInfo ParseTovTorg551BuyerTitleXml(byte[] xmlContent)
		{
			return PerformHttpRequest<TovTorgBuyerTitleInfo>(null, "POST", $"/ParseTorg12BuyerTitleXml?documentVersion={DefaultDocumentVersions.TovTorg551}", xmlContent);
		}

		public AcceptanceCertificateSellerTitleInfo ParseAcceptanceCertificateSellerTitleXml(byte[] xmlContent)
		{
			return PerformHttpRequest<AcceptanceCertificateSellerTitleInfo>(null, "POST", "/ParseAcceptanceCertificateSellerTitleXml", xmlContent);
		}

		public AcceptanceCertificateBuyerTitleInfo ParseAcceptanceCertificateBuyerTitleXml(byte[] xmlContent)
		{
			return PerformHttpRequest<AcceptanceCertificateBuyerTitleInfo>(null, "POST", "/ParseAcceptanceCertificateBuyerTitleXml", xmlContent);
		}

		public AcceptanceCertificate552SellerTitleInfo ParseAcceptanceCertificate552SellerTitleXml(byte[] xmlContent)
		{
			return PerformHttpRequest<AcceptanceCertificate552SellerTitleInfo>(null, "POST", $"/ParseAcceptanceCertificateSellerTitleXml?documentVersion={DefaultDocumentVersions.AcceptanceCerttificate552}", xmlContent);
		}

		public AcceptanceCertificate552BuyerTitleInfo ParseAcceptanceCertificate552BuyerTitleXml(byte[] xmlContent)
		{
			return PerformHttpRequest<AcceptanceCertificate552BuyerTitleInfo>(null, "POST", $"/ParseAcceptanceCertificateBuyerTitleXml?documentVersion={DefaultDocumentVersions.AcceptanceCerttificate552}", xmlContent);
		}

		public UniversalTransferDocumentSellerTitleInfo ParseUniversalTransferDocumentSellerTitleXml(byte[] xmlContent, string documentVersion = null)
		{
			var query = new PathAndQueryBuilder("/ParseUniversalTransferDocumentSellerTitleXml");
			query.AddParameter("documentVersion", documentVersion ?? DefaultDocumentVersions.Utd);
			return PerformHttpRequest<UniversalTransferDocumentSellerTitleInfo>(null, "POST", query.BuildPathAndQuery(), xmlContent);
		}

		public UniversalTransferDocumentBuyerTitleInfo ParseUniversalTransferDocumentBuyerTitleXml(byte[] xmlContent)
		{
			return PerformHttpRequest<UniversalTransferDocumentBuyerTitleInfo>(null, "POST", "/ParseUniversalTransferDocumentBuyerTitleXml", xmlContent);
		}

		public UniversalCorrectionDocumentSellerTitleInfo ParseUniversalCorrectionDocumentSellerTitleXml(byte[] xmlContent, string documentVersion = null)
		{
			var query = new PathAndQueryBuilder("/ParseUniversalCorrectionDocumentSellerTitleXml");
			query.AddParameter("documentVersion", documentVersion ?? DefaultDocumentVersions.Ucd);
			return PerformHttpRequest<UniversalCorrectionDocumentSellerTitleInfo>(null, "POST", query.BuildPathAndQuery(), xmlContent);
		}

		public UniversalTransferDocumentBuyerTitleInfo ParseUniversalCorrectionDocumentBuyerTitleXml(byte[] xmlContent)
		{
			return PerformHttpRequest<UniversalTransferDocumentBuyerTitleInfo>(null, "POST", "/ParseUniversalCorrectionDocumentBuyerTitleXml", xmlContent);
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