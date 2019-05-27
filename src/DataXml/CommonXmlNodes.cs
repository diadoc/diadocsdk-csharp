namespace Diadoc.Api.DataXml
{
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class OtherIssuer
	{
		private string positionField;

		private string employeeInfoField;

		private string employeeBaseField;

		private string organizationNameField;

		private string organizationBaseField;

		private string lastNameField;

		private string firstNameField;

		private string middleNameField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string Position
		{
			get { return this.positionField; }
			set { this.positionField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string EmployeeInfo
		{
			get { return this.employeeInfoField; }
			set { this.employeeInfoField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string EmployeeBase
		{
			get { return this.employeeBaseField; }
			set { this.employeeBaseField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string OrganizationName
		{
			get { return this.organizationNameField; }
			set { this.organizationNameField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string OrganizationBase
		{
			get { return this.organizationBaseField; }
			set { this.organizationBaseField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string LastName
		{
			get { return this.lastNameField; }
			set { this.lastNameField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string FirstName
		{
			get { return this.firstNameField; }
			set { this.firstNameField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string MiddleName
		{
			get { return this.middleNameField; }
			set { this.middleNameField = value; }
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class Employee
	{
		private string positionField;

		private string employeeInfoField;

		private string employeeBaseField;

		private string lastNameField;

		private string firstNameField;

		private string middleNameField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string Position
		{
			get { return this.positionField; }
			set { this.positionField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string EmployeeInfo
		{
			get { return this.employeeInfoField; }
			set { this.employeeInfoField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string EmployeeBase
		{
			get { return this.employeeBaseField; }
			set { this.employeeBaseField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string LastName
		{
			get { return this.lastNameField; }
			set { this.lastNameField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string FirstName
		{
			get { return this.firstNameField; }
			set { this.firstNameField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string MiddleName
		{
			get { return this.middleNameField; }
			set { this.middleNameField = value; }
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class AdditionalInfoId
	{
		private AdditionalInfo[] additionalInfoField;

		private string infoFileIdField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("AdditionalInfo", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public AdditionalInfo[] AdditionalInfo
		{
			get { return this.additionalInfoField; }
			set { this.additionalInfoField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string InfoFileId
		{
			get { return this.infoFileIdField; }
			set { this.infoFileIdField = value; }
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class AdditionalInfo
	{
		private string idField;

		private string valueField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string Id
		{
			get { return this.idField; }
			set { this.idField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string Value
		{
			get { return this.valueField; }
			set { this.valueField = value; }
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtendedSignerDetails_BuyerTitle820))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtendedSignerDetails))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtendedSignerDetails_551_552))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtendedSignerDetails_CorrectionBuyerTitle))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtendedSignerDetails_CorrectionSellerTitle))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtendedSignerDetails_BuyerTitle))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtendedSignerDetails_SellerTitle))]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class ExtendedSignerDetailsBase
	{
		private string lastNameField;

		private string firstNameField;

		private string middleNameField;

		private string positionField;

		private string innField;

		private string registrationCertificateField;

		private ExtendedSignerDetailsBaseSignerType signerTypeField;

		private string signerOrganizationNameField;

		private string signerInfoField;

		private string signerPowersBaseField;

		private string signerOrgPowersBaseField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string LastName
		{
			get { return this.lastNameField; }
			set { this.lastNameField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string FirstName
		{
			get { return this.firstNameField; }
			set { this.firstNameField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string MiddleName
		{
			get { return this.middleNameField; }
			set { this.middleNameField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string Position
		{
			get { return this.positionField; }
			set { this.positionField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string Inn
		{
			get { return this.innField; }
			set { this.innField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string RegistrationCertificate
		{
			get { return this.registrationCertificateField; }
			set { this.registrationCertificateField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public ExtendedSignerDetailsBaseSignerType SignerType
		{
			get { return this.signerTypeField; }
			set { this.signerTypeField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string SignerOrganizationName
		{
			get { return this.signerOrganizationNameField; }
			set { this.signerOrganizationNameField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string SignerInfo
		{
			get { return this.signerInfoField; }
			set { this.signerInfoField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string SignerPowersBase
		{
			get { return this.signerPowersBaseField; }
			set { this.signerPowersBaseField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string SignerOrgPowersBase
		{
			get { return this.signerOrgPowersBaseField; }
			set { this.signerOrgPowersBaseField = value; }
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public enum ExtendedSignerDetailsBaseSignerType
	{
		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("1")]
		LegalEntity,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("2")]
		IndividualEntity,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("3")]
		PhysicalPerson,
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class ExtendedSignerDetails_BuyerTitle820 : ExtendedSignerDetailsBase
	{
		private ExtendedSignerDetails_BuyerTitle820SignerPowers signerPowersField;

		private ExtendedSignerDetails_BuyerTitle820SignerStatus signerStatusField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public ExtendedSignerDetails_BuyerTitle820SignerPowers SignerPowers
		{
			get { return this.signerPowersField; }
			set { this.signerPowersField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public ExtendedSignerDetails_BuyerTitle820SignerStatus SignerStatus
		{
			get { return this.signerStatusField; }
			set { this.signerStatusField = value; }
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public enum ExtendedSignerDetails_BuyerTitle820SignerPowers
	{
		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("1")]
		PersonMadeOperation,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("2")]
		MadeAndSignOperation,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("3")]
		PersonDocumentedOperation,
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public enum ExtendedSignerDetails_BuyerTitle820SignerStatus
	{
		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("3")]
		OtherOrganizationEmployee,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("4")]
		AuthorizedPerson,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("5")]
		BuyerEmployee,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("6")]
		InformationCreatorBuyerEmployee,
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtendedSignerDetails_551_552))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtendedSignerDetails_CorrectionBuyerTitle))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtendedSignerDetails_CorrectionSellerTitle))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtendedSignerDetails_BuyerTitle))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtendedSignerDetails_SellerTitle))]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class ExtendedSignerDetails : ExtendedSignerDetailsBase
	{
		private ExtendedSignerDetailsSignerStatus signerStatusField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public ExtendedSignerDetailsSignerStatus SignerStatus
		{
			get { return this.signerStatusField; }
			set { this.signerStatusField = value; }
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public enum ExtendedSignerDetailsSignerStatus
	{
		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("1")]
		SellerEmployee,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("2")]
		InformationCreatorEmployee,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("3")]
		OtherOrganizationEmployee,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("4")]
		AuthorizedPerson,
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class ExtendedSignerDetails_551_552 : ExtendedSignerDetails
	{
		private ExtendedSignerDetails_551_552SignerPowers signerPowersField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public ExtendedSignerDetails_551_552SignerPowers SignerPowers
		{
			get { return this.signerPowersField; }
			set { this.signerPowersField = value; }
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public enum ExtendedSignerDetails_551_552SignerPowers
	{
		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("1")]
		PersonMadeOperation,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("2")]
		MadeAndSignOperation,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("3")]
		PersonDocumentedOperation,
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class ExtendedSignerDetails_CorrectionBuyerTitle : ExtendedSignerDetails
	{
		private ExtendedSignerDetails_CorrectionBuyerTitleSignerPowers signerPowersField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public ExtendedSignerDetails_CorrectionBuyerTitleSignerPowers SignerPowers
		{
			get { return this.signerPowersField; }
			set { this.signerPowersField = value; }
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public enum ExtendedSignerDetails_CorrectionBuyerTitleSignerPowers
	{
		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("3")]
		PersonDocumentedOperation,
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class ExtendedSignerDetails_CorrectionSellerTitle : ExtendedSignerDetails
	{
		private ExtendedSignerDetails_CorrectionSellerTitleSignerPowers signerPowersField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public ExtendedSignerDetails_CorrectionSellerTitleSignerPowers SignerPowers
		{
			get { return this.signerPowersField; }
			set { this.signerPowersField = value; }
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public enum ExtendedSignerDetails_CorrectionSellerTitleSignerPowers
	{
		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("0")]
		InvoiceSigner,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("3")]
		PersonDocumentedOperation,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("6")]
		ResponsibleForOperationAndSignerForInvoice,
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class ExtendedSignerDetails_BuyerTitle : ExtendedSignerDetails
	{
		private ExtendedSignerDetails_BuyerTitleSignerPowers signerPowersField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public ExtendedSignerDetails_BuyerTitleSignerPowers SignerPowers
		{
			get { return this.signerPowersField; }
			set { this.signerPowersField = value; }
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public enum ExtendedSignerDetails_BuyerTitleSignerPowers
	{
		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("1")]
		PersonMadeOperation,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("2")]
		MadeAndSignOperation,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("3")]
		PersonDocumentedOperation,
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class ExtendedSignerDetails_SellerTitle : ExtendedSignerDetails
	{
		private ExtendedSignerDetails_SellerTitleSignerPowers signerPowersField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public ExtendedSignerDetails_SellerTitleSignerPowers SignerPowers
		{
			get { return this.signerPowersField; }
			set { this.signerPowersField = value; }
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public enum ExtendedSignerDetails_SellerTitleSignerPowers
	{
		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("0")]
		InvoiceSigner,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("1")]
		PersonMadeOperation,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("2")]
		MadeAndSignOperation,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("3")]
		PersonDocumentedOperation,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("4")]
		MadeOperationAndSignedInvoice,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("5")]
		MadeAndResponsibleForOperationAndSignedInvoice,

		/// <remarks/>
		[System.Xml.Serialization.XmlEnumAttribute("6")]
		ResponsibleForOperationAndSignerForInvoice,
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	public partial class SignerReference
	{
		private string boxIdField;

		private byte[] certificateBytesField;

		private string certificateThumbprintField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string BoxId
		{
			get { return this.boxIdField; }
			set { this.boxIdField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(DataType = "base64Binary")]
		public byte[] CertificateBytes
		{
			get { return this.certificateBytesField; }
			set { this.certificateBytesField = value; }
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string CertificateThumbprint
		{
			get { return this.certificateThumbprintField; }
			set { this.certificateThumbprintField = value; }
		}
	}
}