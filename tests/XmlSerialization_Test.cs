using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Diadoc.Api.Proto.Invoicing;
using NUnit.Framework;

namespace Diadoc.Api.Tests
{
	[TestFixture]
	public class XmlSerialization_Test
	{
		[Test]
		public void InvoiceInfo_CanBeSerialized()
		{
			var info = new InvoiceInfo();
			AssertInfoSerialized(info);
		}

		[Test]
		public void InvoiceRevisionInfo_CanBeSerialized()
		{
			var info = new InvoiceCorrectionInfo();
			AssertInfoSerialized(info);
		}

		[Test]
		public void Torg12SellerTitleInfo_CanBeSerialized()
		{
			var info = new Torg12SellerTitleInfo();
			AssertInfoSerialized(info);
		}

		[Test]
		public void Torg12BuyerTitleInfo_CanBeSerialized()
		{
			var info = new Torg12BuyerTitleInfo();
			AssertInfoSerialized(info);
		}

		[Test]
		public void AcceptanceCertificateSellerTitleInfo_CanBeSerialized()
		{
			var info = new AcceptanceCertificateSellerTitleInfo();
			AssertInfoSerialized(info);
		}

		[Test]
		public void AcceptanceCertificateBuyerTitleInfo_CanBeSerialized()
		{
			var info = new AcceptanceCertificateBuyerTitleInfo();
			AssertInfoSerialized(info);
		}

		[Test]
		public void UniversalTransferDocumentSellerTitleInfo_CanBeSerialized()
		{
			var info = new UniversalTransferDocumentSellerTitleInfo();
			AssertInfoSerialized(info);
		}

		[Test]
		public void UniversalCorrectionDocumentSellerTitleInfo_CanBeSerialized()
		{
			var info = new UniversalCorrectionDocumentSellerTitleInfo();
			AssertInfoSerialized(info);
		}

		[Test]
		public void UniversalTransferDocumentBuyerTitleInfo_CanBeSerialized()
		{
			var info = new UniversalTransferDocumentBuyerTitleInfo();
			AssertInfoSerialized(info);
		}

		private void AssertInfoSerialized<T>(T info)
		{
			var serializer = GetSerializer(info);
			var memoryStream = new MemoryStream();
			serializer.Serialize(memoryStream, info);
			Console.WriteLine(Encoding.UTF8.GetString(memoryStream.ToArray()));
		}

		private static XmlSerializer GetSerializer<T>(T info)
		{
			return new XmlSerializer(info.GetType());
		}
	}

}