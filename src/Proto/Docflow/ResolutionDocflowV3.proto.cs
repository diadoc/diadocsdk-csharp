//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Docflow/ResolutionDocflowV3.proto
// Note: requires additional types generated from: Timestamp.proto
// Note: requires additional types generated from: ResolutionAction.proto
// Note: requires additional types generated from: ResolutionRequestType.proto
// Note: requires additional types generated from: ResolutionTarget.proto
// Note: requires additional types generated from: ResolutionType.proto
// Note: requires additional types generated from: Docflow/Attachment.proto
// Note: requires additional types generated from: Docflow/AttachmentV3.proto
namespace Diadoc.Api.Proto.Docflow
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ResolutionDocflowV3")]
  public partial class ResolutionDocflowV3 : global::ProtoBuf.IExtensible
  {
    public ResolutionDocflowV3() {}
    
    private bool _IsFinished;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"IsFinished", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool IsFinished
    {
      get { return _IsFinished; }
      set { _IsFinished = value; }
    }
    private string _ParentEntityId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"ParentEntityId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string ParentEntityId
    {
      get { return _ParentEntityId; }
      set { _ParentEntityId = value; }
    }
    private Diadoc.Api.Proto.Docflow.ResolutionStatus _ResolutionStatus;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"ResolutionStatus", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public Diadoc.Api.Proto.Docflow.ResolutionStatus ResolutionStatus
    {
      get { return _ResolutionStatus; }
      set { _ResolutionStatus = value; }
    }

    private string _ResolutionEntityId = "";
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"ResolutionEntityId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ResolutionEntityId
    {
      get { return _ResolutionEntityId; }
      set { _ResolutionEntityId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ResolutionEntitiesV3")]
  public partial class ResolutionEntitiesV3 : global::ProtoBuf.IExtensible
  {
    public ResolutionEntitiesV3() {}
    
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.ResolutionRequestV3> _Requests = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.ResolutionRequestV3>();
    [global::ProtoBuf.ProtoMember(1, Name=@"Requests", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.ResolutionRequestV3> Requests
    {
      get { return _Requests; }
    }
  
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.ResolutionV3> _Resolutions = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.ResolutionV3>();
    [global::ProtoBuf.ProtoMember(2, Name=@"Resolutions", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.ResolutionV3> Resolutions
    {
      get { return _Resolutions; }
    }
  
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.ApprovementSignatureV3> _ApprovementSignatures = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.ApprovementSignatureV3>();
    [global::ProtoBuf.ProtoMember(3, Name=@"ApprovementSignatures", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.ApprovementSignatureV3> ApprovementSignatures
    {
      get { return _ApprovementSignatures; }
    }
  
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.SignatureDenialV3> _SignatureDenials = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.SignatureDenialV3>();
    [global::ProtoBuf.ProtoMember(4, Name=@"SignatureDenials", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.SignatureDenialV3> SignatureDenials
    {
      get { return _SignatureDenials; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ResolutionRequestV3")]
  public partial class ResolutionRequestV3 : global::ProtoBuf.IExtensible
  {
    public ResolutionRequestV3() {}
    
    private Diadoc.Api.Proto.Docflow.Entity _Entity;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"Entity", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Diadoc.Api.Proto.Docflow.Entity Entity
    {
      get { return _Entity; }
      set { _Entity = value; }
    }
    private Diadoc.Api.Proto.ResolutionTarget _Target;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"Target", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Diadoc.Api.Proto.ResolutionTarget Target
    {
      get { return _Target; }
      set { _Target = value; }
    }

    private string _AuthorUserId = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"AuthorUserId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string AuthorUserId
    {
      get { return _AuthorUserId; }
      set { _AuthorUserId = value; }
    }
    private Diadoc.Api.Proto.ResolutionRequestType _RequestType;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"RequestType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public Diadoc.Api.Proto.ResolutionRequestType RequestType
    {
      get { return _RequestType; }
      set { _RequestType = value; }
    }

    private string _ResolvedWith = "";
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"ResolvedWith", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ResolvedWith
    {
      get { return _ResolvedWith; }
      set { _ResolvedWith = value; }
    }
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.ResolutionAction> _Actions = new global::System.Collections.Generic.List<Diadoc.Api.Proto.ResolutionAction>();
    [global::ProtoBuf.ProtoMember(6, Name=@"Actions", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.ResolutionAction> Actions
    {
      get { return _Actions; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ResolutionV3")]
  public partial class ResolutionV3 : global::ProtoBuf.IExtensible
  {
    public ResolutionV3() {}
    
    private Diadoc.Api.Proto.Docflow.Entity _Entity;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"Entity", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Diadoc.Api.Proto.Docflow.Entity Entity
    {
      get { return _Entity; }
      set { _Entity = value; }
    }

    private string _ResolutionRequestId = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"ResolutionRequestId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ResolutionRequestId
    {
      get { return _ResolutionRequestId; }
      set { _ResolutionRequestId = value; }
    }

    private string _AuthorUserId = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"AuthorUserId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string AuthorUserId
    {
      get { return _AuthorUserId; }
      set { _AuthorUserId = value; }
    }
    private Diadoc.Api.Proto.ResolutionType _ResolutionType;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"ResolutionType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public Diadoc.Api.Proto.ResolutionType ResolutionType
    {
      get { return _ResolutionType; }
      set { _ResolutionType = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ApprovementSignatureV3")]
  public partial class ApprovementSignatureV3 : global::ProtoBuf.IExtensible
  {
    public ApprovementSignatureV3() {}
    
    private Diadoc.Api.Proto.Docflow.SignatureV3 _Signature;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"Signature", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Diadoc.Api.Proto.Docflow.SignatureV3 Signature
    {
      get { return _Signature; }
      set { _Signature = value; }
    }

    private string _ResolutionRequestId = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"ResolutionRequestId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ResolutionRequestId
    {
      get { return _ResolutionRequestId; }
      set { _ResolutionRequestId = value; }
    }

    private string _AuthorUserId = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"AuthorUserId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string AuthorUserId
    {
      get { return _AuthorUserId; }
      set { _AuthorUserId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SignatureDenialV3")]
  public partial class SignatureDenialV3 : global::ProtoBuf.IExtensible
  {
    public SignatureDenialV3() {}
    
    private Diadoc.Api.Proto.Docflow.Entity _Entity;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"Entity", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Diadoc.Api.Proto.Docflow.Entity Entity
    {
      get { return _Entity; }
      set { _Entity = value; }
    }
    private string _ResolutionRequestId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"ResolutionRequestId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string ResolutionRequestId
    {
      get { return _ResolutionRequestId; }
      set { _ResolutionRequestId = value; }
    }

    private string _AuthorUserId = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"AuthorUserId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string AuthorUserId
    {
      get { return _AuthorUserId; }
      set { _AuthorUserId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"ResolutionStatus")]
    public enum ResolutionStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"UnknownStatus", Value=0)]
      UnknownStatus = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"None", Value=1)]
      None = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Approved", Value=2)]
      Approved = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Disapproved", Value=3)]
      Disapproved = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ApprovementRequested", Value=4)]
      ApprovementRequested = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ApprovementSignatureRequested", Value=5)]
      ApprovementSignatureRequested = 5,
            
      [global::ProtoBuf.ProtoEnum(Name=@"PrimarySignatureRequested", Value=6)]
      PrimarySignatureRequested = 6,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SignatureRequestRejected", Value=7)]
      SignatureRequestRejected = 7,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SignedWithApprovingSignature", Value=8)]
      SignedWithApprovingSignature = 8,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SignedWithPrimarySignature", Value=9)]
      SignedWithPrimarySignature = 9,
            
      [global::ProtoBuf.ProtoEnum(Name=@"PrimarySignatureRejected", Value=10)]
      PrimarySignatureRejected = 10,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ActionsRequested", Value=11)]
      ActionsRequested = 11
    }
  
}