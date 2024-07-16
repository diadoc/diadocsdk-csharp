//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: PowersOfAttorney/PowerOfAttorneyValidation.proto
// Note: requires additional types generated from: Severity.proto
// Note: requires additional types generated from: Content_v3.proto
namespace Diadoc.Api.Proto.PowersOfAttorney
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PowerOfAttorneyValidationStatus")]
  public partial class PowerOfAttorneyValidationStatus : global::ProtoBuf.IExtensible
  {
    public PowerOfAttorneyValidationStatus() {}
    

    private Diadoc.Api.Proto.Severity _Severity = Diadoc.Api.Proto.Severity.UnknownSeverity;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Severity", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(Diadoc.Api.Proto.Severity.UnknownSeverity)]
    public Diadoc.Api.Proto.Severity Severity
    {
      get { return _Severity; }
      set { _Severity = value; }
    }

    private Diadoc.Api.Proto.PowersOfAttorney.PowerOfAttorneyValidationStatusNamedId _StatusNamedId = Diadoc.Api.Proto.PowersOfAttorney.PowerOfAttorneyValidationStatusNamedId.UnknownStatus;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"StatusNamedId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(Diadoc.Api.Proto.PowersOfAttorney.PowerOfAttorneyValidationStatusNamedId.UnknownStatus)]
    public Diadoc.Api.Proto.PowersOfAttorney.PowerOfAttorneyValidationStatusNamedId StatusNamedId
    {
      get { return _StatusNamedId; }
      set { _StatusNamedId = value; }
    }

    private string _StatusText = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"StatusText", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string StatusText
    {
      get { return _StatusText; }
      set { _StatusText = value; }
    }
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.PowersOfAttorney.PowerOfAttorneyValidationError> _Errors = new global::System.Collections.Generic.List<Diadoc.Api.Proto.PowersOfAttorney.PowerOfAttorneyValidationError>();
    [global::System.Obsolete, global::ProtoBuf.ProtoMember(4, Name=@"Errors", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.PowersOfAttorney.PowerOfAttorneyValidationError> Errors
    {
      get { return _Errors; }
    }
  

    private Diadoc.Api.Proto.PowersOfAttorney.ValidationProtocol _ValidationProtocol = null;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"ValidationProtocol", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.PowersOfAttorney.ValidationProtocol ValidationProtocol
    {
      get { return _ValidationProtocol; }
      set { _ValidationProtocol = value; }
    }

    private Diadoc.Api.Proto.PowersOfAttorney.PowerOfAttorneyValidationError _OperationError = null;
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"OperationError", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.PowersOfAttorney.PowerOfAttorneyValidationError OperationError
    {
      get { return _OperationError; }
      set { _OperationError = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PowerOfAttorneyValidationError")]
  public partial class PowerOfAttorneyValidationError : global::ProtoBuf.IExtensible
  {
    public PowerOfAttorneyValidationError() {}
    
    private string _Code;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"Code", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string Code
    {
      get { return _Code; }
      set { _Code = value; }
    }
    private string _Text;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"Text", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string Text
    {
      get { return _Text; }
      set { _Text = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PowerOfAttorneyPrevalidateRequest")]
  public partial class PowerOfAttorneyPrevalidateRequest : global::ProtoBuf.IExtensible
  {
    public PowerOfAttorneyPrevalidateRequest() {}
    
    private Diadoc.Api.Proto.PowersOfAttorney.ConfidantCertificateToPrevalidate _ConfidantCertificate;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"ConfidantCertificate", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Diadoc.Api.Proto.PowersOfAttorney.ConfidantCertificateToPrevalidate ConfidantCertificate
    {
      get { return _ConfidantCertificate; }
      set { _ConfidantCertificate = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ConfidantCertificateToPrevalidate")]
  public partial class ConfidantCertificateToPrevalidate : global::ProtoBuf.IExtensible
  {
    public ConfidantCertificateToPrevalidate() {}
    

    private string _Thumbprint = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Thumbprint", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Thumbprint
    {
      get { return _Thumbprint; }
      set { _Thumbprint = value; }
    }

    private Diadoc.Api.Proto.Content_v3 _Content = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Content", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.Content_v3 Content
    {
      get { return _Content; }
      set { _Content = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PowerOfAttorneyPrevalidateResult")]
  public partial class PowerOfAttorneyPrevalidateResult : global::ProtoBuf.IExtensible
  {
    public PowerOfAttorneyPrevalidateResult() {}
    
    private Diadoc.Api.Proto.PowersOfAttorney.PowerOfAttorneyValidationStatus _PrevalidateStatus;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"PrevalidateStatus", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Diadoc.Api.Proto.PowersOfAttorney.PowerOfAttorneyValidationStatus PrevalidateStatus
    {
      get { return _PrevalidateStatus; }
      set { _PrevalidateStatus = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ValidationProtocol")]
  public partial class ValidationProtocol : global::ProtoBuf.IExtensible
  {
    public ValidationProtocol() {}
    
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.PowersOfAttorney.ValidationCheckResult> _CheckResults = new global::System.Collections.Generic.List<Diadoc.Api.Proto.PowersOfAttorney.ValidationCheckResult>();
    [global::ProtoBuf.ProtoMember(1, Name=@"CheckResults", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.PowersOfAttorney.ValidationCheckResult> CheckResults
    {
      get { return _CheckResults; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ValidationCheckResult")]
  public partial class ValidationCheckResult : global::ProtoBuf.IExtensible
  {
    public ValidationCheckResult() {}
    
    private string _Status;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"Status", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string Status
    {
      get { return _Status; }
      set { _Status = value; }
    }
    private string _Name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"Name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string Name
    {
      get { return _Name; }
      set { _Name = value; }
    }

    private Diadoc.Api.Proto.PowersOfAttorney.PowerOfAttorneyValidationError _Error = null;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Error", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.PowersOfAttorney.PowerOfAttorneyValidationError Error
    {
      get { return _Error; }
      set { _Error = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"PowerOfAttorneyValidationStatusNamedId")]
    public enum PowerOfAttorneyValidationStatusNamedId
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"UnknownStatus", Value=0)]
      UnknownStatus = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CanNotBeValidated", Value=1)]
      CanNotBeValidated = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"IsValid", Value=2)]
      IsValid = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"IsNotValid", Value=3)]
      IsNotValid = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ValidationError", Value=4)]
      ValidationError = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"IsNotAttached", Value=5)]
      IsNotAttached = 5,
            
      [global::ProtoBuf.ProtoEnum(Name=@"HasWarnings", Value=6)]
      HasWarnings = 6
    }
  
}
