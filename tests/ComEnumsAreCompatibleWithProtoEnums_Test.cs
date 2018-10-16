using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Docflow;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Documents.BilateralDocument;
using Diadoc.Api.Proto.Documents.InvoiceDocument;
using Diadoc.Api.Proto.Documents.NonformalizedDocument;
using Diadoc.Api.Proto.Documents.Types;
using Diadoc.Api.Proto.Documents.UnilateralDocument;
using Diadoc.Api.Proto.Documents.UniversalTransferDocument;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;
using Diadoc.Api.Proto.Invoicing.Organizations;
using Diadoc.Api.Proto.Invoicing.Signers;
using NUnit.Framework;
using SignerType = Diadoc.Api.Proto.Invoicing.Signers.SignerType;

namespace Diadoc.Api.Tests
{
	[TestFixture]
	public class ComEnumsAreCompatibleWithProtoEnums_Test
	{
		[Test]
		public void Test_ReceiptStatus()
		{
			AssertEnumsAreCompatible(
				typeof(Com.ReceiptStatus),
				typeof(ReceiptStatus));
		}

		[Test]
		public void Test_BilateralDocumentStatus()
		{
			AssertEnumsAreCompatible(
				typeof(Com.BilateralDocumentStatus),
				typeof(BilateralDocumentStatus));
		}

		[Test]
		public void Test_RevocationStatus()
		{
			AssertEnumsAreCompatible(
				typeof(Com.RevocationStatus),
				typeof(RevocationStatus));
		}

		[Test]
		public void Test_ResolutionRequestType()
		{
			AssertEnumsAreCompatible(
				typeof(Com.ResolutionRequestType),
				typeof(ResolutionRequestType));
		}

		[Test]
		public void Test_UnilateralDocumentStatus()
		{
			AssertEnumsAreCompatible(
				typeof(Com.UnilateralDocumentStatus),
				typeof(UnilateralDocumentStatus));
		}

		[Test]
		public void Test_UniversalTransferDocumentStatus()
		{
			AssertEnumsAreCompatible(
				typeof(Com.UniversalTransferDocumentStatus),
				typeof(UniversalTransferDocumentStatus));
		}

		[Test]
		public void Test_InvoiceStatus()
		{
			AssertEnumsAreCompatible(
				typeof(Com.InvoiceStatus),
				typeof(InvoiceStatus));
		}

		[Test]
		public void Test_ItemMark()
		{
			AssertEnumsAreCompatible(
				typeof(Com.ItemMark),
				typeof(ItemMark));
		}

		[Test]
		public void Test_TaxRate()
		{
			AssertEnumsAreCompatible(
				typeof(Com.TaxRate),
				typeof(TaxRate));
		}

		[Test]
		public void Test_EntityType()
		{
			AssertEnumsAreCompatible(
				typeof(Com.EntityType),
				typeof(EntityType));
		}

		[Test]
		public void Test_FunctionType()
		{
			AssertEnumsAreCompatible(
				typeof(Com.FunctionType),
				typeof(FunctionType));
		}

		[Test]
		public void Test_AttachmentType()
		{
			AssertEnumsAreCompatible(
				typeof(Com.AttachmentType),
				typeof(AttachmentType));
		}

		[Test]
		public void Test_DocumentAccessLevel()
		{
			AssertEnumsAreCompatible(
				typeof(Com.DocumentAccessLevel),
				typeof(DocumentAccessLevel));
		}

		[Test]
		public void Test_DocumentType()
		{
			AssertEnumsAreCompatible(
				typeof(Com.DocumentType),
				typeof(DocumentType));
		}

		[Test]
		public void Test_CertificateChainStatus()
		{
			AssertFirstEnumIsMoreOrEquals(
				typeof(Com.CertificateChainStatus),
				typeof(X509ChainStatusFlags));
		}

		[Test]
		public void Test_NonformalizedDocumentStatus()
		{
			AssertEnumsAreCompatible(
				typeof(Com.NonformalizedDocumentStatus),
				typeof(NonformalizedDocumentStatus));
		}

		[Test]
		public void Test_OrgType()
		{
			AssertEnumsAreCompatible(
				typeof(Com.OrgType),
				typeof(OrgType));
		}

		[Test]
		public void Test_SearchScope()
		{
			AssertEnumsAreCompatible(
				typeof(Com.SearchScope),
				typeof(SearchScope));
		}

		[Test]
		public void Test_SignerPowers()
		{
			AssertEnumsAreCompatible(
				typeof(Com.SignerPowers),
				typeof(SignerPowers));
		}

		[Test]
		public void Test_SignerStatus()
		{
			AssertEnumsAreCompatible(
				typeof(Com.SignerStatus),
				typeof(SignerStatus));
		}

		[Test]
		public void Test_SignerType()
		{
			AssertEnumsAreCompatible(
				typeof(Com.SignerType),
				typeof(SignerType));
		}

		[Test]
		public void Test_DocumentTitleSignerType()
		{
			AssertEnumsAreCompatible(
				typeof(Com.DocumentTitleSignerType),
				typeof(Diadoc.Api.Proto.Documents.Types.SignerType));
		}

		[Test]
		public void Test_DocumentTitleType()
		{
			AssertEnumsAreCompatible(
				typeof(Com.DocumentTitleType),
				typeof(DocumentTitleType));
		}

		[Test]
		public void Test_DocumentDocflow()
		{
			AssertEnumsAreCompatible(
				typeof(Com.DocumentDocflow),
				typeof(DocumentDocflow));
		}

		[Test]
		public void Test_DocumentMetadataItemType()
		{
			AssertEnumsAreCompatible(
				typeof(Com.DocumentMetadataItemType),
				typeof(DocumentMetadataItemType));
		}

		[Test]
		public void Test_DocumentMetadataSource()
		{
			AssertEnumsAreCompatible(
				typeof(Com.DocumentMetadataSource),
				typeof(DocumentMetadataSource));
		}

		[Test]
		public void Test_OrganizationInvoiceFormatVersion()
		{
			AssertEnumsAreCompatible(
				typeof(Com.OrganizationInvoiceFormatVersion),
				typeof(OrganizationInvoiceFormatVersion));
		}

		[Test]
		public void Test_CustomDataPatchOperation()
		{
			AssertEnumsAreCompatible(
				typeof(Com.CustomDataPatchOperation),
				typeof(CustomDataPatchOperation));
		}

		private static void AssertEnumsAreCompatible(Type comEnumType, Type protoEnumType)
		{
			var comEnumValues = GetEnumValues(comEnumType);
			var protoEnumValues = GetEnumValues(protoEnumType);
			Assert.That(comEnumValues, Is.EquivalentTo(protoEnumValues));

			foreach (var valueName in protoEnumValues)
			{
				var comValue = (int)comEnumType.GetField(valueName).GetValue(null);
				var protoValue = (int)protoEnumType.GetField(valueName).GetValue(null);
				Assert.AreEqual(comValue, protoValue);
			}
		}

		private static void AssertFirstEnumIsMoreOrEquals(Type first, Type second)
		{
			var firstValues = GetEnumValues(first);
			var secondValues = GetEnumValues(second);

			var subractedList = secondValues.Except(firstValues);
			Assert.IsEmpty(subractedList);
		}

		private static string[] GetEnumValues(Type enumType)
		{
			return enumType
				.GetMembers(BindingFlags.Public | BindingFlags.Static)
				.Select(x => x.Name).ToArray();
		}
	}
}