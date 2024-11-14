﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.8.3928.0.
// 
namespace Diadoc.Api.DataXml.ON_NKORSCHFDOPPOK_UserContract_1_996_04_05_01_03 {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class UniversalCorrectionDocumentBuyerTitle {
        
        private AdditionalInfoId736 additionalInfoIdField;
        
        private object[] signersField;
        
        private string documentCreatorField;
        
        private string documentCreatorBaseField;
        
        private string operationContentField;
        
        private string acceptanceDateField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AdditionalInfoId736 AdditionalInfoId {
            get {
                return this.additionalInfoIdField;
            }
            set {
                this.additionalInfoIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SignerDetails", typeof(ExtendedSignerDetails_CorrectionBuyerTitle736), Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SignerReference", typeof(SignerReferenceWithPowerOfAttorney), Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public object[] Signers {
            get {
                return this.signersField;
            }
            set {
                this.signersField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DocumentCreator {
            get {
                return this.documentCreatorField;
            }
            set {
                this.documentCreatorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DocumentCreatorBase {
            get {
                return this.documentCreatorBaseField;
            }
            set {
                this.documentCreatorBaseField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string OperationContent {
            get {
                return this.operationContentField;
            }
            set {
                this.operationContentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AcceptanceDate {
            get {
                return this.acceptanceDateField;
            }
            set {
                this.acceptanceDateField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AdditionalInfoId736 {
        
        private AdditionalInfo[] additionalInfoField;
        
        private string infoFileIdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AdditionalInfo", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public AdditionalInfo[] AdditionalInfo {
            get {
                return this.additionalInfoField;
            }
            set {
                this.additionalInfoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InfoFileId {
            get {
                return this.infoFileIdField;
            }
            set {
                this.infoFileIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AdditionalInfo {
        
        private string idField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtendedSignerDetails736))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtendedSignerDetails_CorrectionBuyerTitle736))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExtendedSignerDetailsBase736 {
        
        private string lastNameField;
        
        private string firstNameField;
        
        private string middleNameField;
        
        private string positionField;
        
        private string innField;
        
        private string registrationCertificateField;
        
        private ExtendedSignerDetailsBase736SignerType signerTypeField;
        
        private string signerOrganizationNameField;
        
        private string signerInfoField;
        
        private string signerPowersBaseField;
        
        private string signerOrgPowersBaseField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LastName {
            get {
                return this.lastNameField;
            }
            set {
                this.lastNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FirstName {
            get {
                return this.firstNameField;
            }
            set {
                this.firstNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MiddleName {
            get {
                return this.middleNameField;
            }
            set {
                this.middleNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Position {
            get {
                return this.positionField;
            }
            set {
                this.positionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Inn {
            get {
                return this.innField;
            }
            set {
                this.innField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RegistrationCertificate {
            get {
                return this.registrationCertificateField;
            }
            set {
                this.registrationCertificateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ExtendedSignerDetailsBase736SignerType SignerType {
            get {
                return this.signerTypeField;
            }
            set {
                this.signerTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SignerOrganizationName {
            get {
                return this.signerOrganizationNameField;
            }
            set {
                this.signerOrganizationNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SignerInfo {
            get {
                return this.signerInfoField;
            }
            set {
                this.signerInfoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SignerPowersBase {
            get {
                return this.signerPowersBaseField;
            }
            set {
                this.signerPowersBaseField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SignerOrgPowersBase {
            get {
                return this.signerOrgPowersBaseField;
            }
            set {
                this.signerOrgPowersBaseField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ExtendedSignerDetailsBase736SignerType {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtendedSignerDetails_CorrectionBuyerTitle736))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExtendedSignerDetails736 : ExtendedSignerDetailsBase736 {
        
        private ExtendedSignerDetails736SignerStatus signerStatusField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ExtendedSignerDetails736SignerStatus SignerStatus {
            get {
                return this.signerStatusField;
            }
            set {
                this.signerStatusField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ExtendedSignerDetails736SignerStatus {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ExtendedSignerDetails_CorrectionBuyerTitle736 : ExtendedSignerDetails736 {
        
        private ExtendedSignerDetails_CorrectionBuyerTitle736SignerPowers signerPowersField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ExtendedSignerDetails_CorrectionBuyerTitle736SignerPowers SignerPowers {
            get {
                return this.signerPowersField;
            }
            set {
                this.signerPowersField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ExtendedSignerDetails_CorrectionBuyerTitle736SignerPowers {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("29")]
        Item29,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PowerOfAttorney {
        
        private PowerOfAttorneyFullId fullIdField;
        
        private PowerOfAttorneyUseDefault useDefaultField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PowerOfAttorneyFullId FullId {
            get {
                return this.fullIdField;
            }
            set {
                this.fullIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public PowerOfAttorneyUseDefault UseDefault {
            get {
                return this.useDefaultField;
            }
            set {
                this.useDefaultField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class PowerOfAttorneyFullId {
        
        private string registrationNumberField;
        
        private string issuerInnField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RegistrationNumber {
            get {
                return this.registrationNumberField;
            }
            set {
                this.registrationNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IssuerInn {
            get {
                return this.issuerInnField;
            }
            set {
                this.issuerInnField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum PowerOfAttorneyUseDefault {
        
        /// <remarks/>
        @true,
        
        /// <remarks/>
        @false,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SignerReferenceWithPowerOfAttorney {
        
        private PowerOfAttorney powerOfAttorneyField;
        
        private string boxIdField;
        
        private byte[] certificateBytesField;
        
        private string certificateThumbprintField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public PowerOfAttorney PowerOfAttorney {
            get {
                return this.powerOfAttorneyField;
            }
            set {
                this.powerOfAttorneyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BoxId {
            get {
                return this.boxIdField;
            }
            set {
                this.boxIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="base64Binary")]
        public byte[] CertificateBytes {
            get {
                return this.certificateBytesField;
            }
            set {
                this.certificateBytesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CertificateThumbprint {
            get {
                return this.certificateThumbprintField;
            }
            set {
                this.certificateThumbprintField = value;
            }
        }
    }
}
