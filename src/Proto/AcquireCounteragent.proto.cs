//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: AcquireCounteragent.proto
// Note: requires additional types generated from: Events/DiadocMessage-PostApi.proto
// Note: requires additional types generated from: DocumentId.proto
namespace Diadoc.Api.Proto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"AcquireCounteragentRequest")]
  public partial class AcquireCounteragentRequest : global::ProtoBuf.IExtensible
  {
    public AcquireCounteragentRequest() {}
    

    private string _OrgId = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"OrgId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string OrgId
    {
      get { return _OrgId; }
      set { _OrgId = value; }
    }

    private string _Inn = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Inn", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Inn
    {
      get { return _Inn; }
      set { _Inn = value; }
    }

    private string _MessageToCounteragent = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"MessageToCounteragent", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string MessageToCounteragent
    {
      get { return _MessageToCounteragent; }
      set { _MessageToCounteragent = value; }
    }

    private Diadoc.Api.Proto.InvitationDocument _InvitationDocument = null;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"InvitationDocument", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.InvitationDocument InvitationDocument
    {
      get { return _InvitationDocument; }
      set { _InvitationDocument = value; }
    }

    private string _BoxId = "";
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"BoxId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string BoxId
    {
      get { return _BoxId; }
      set { _BoxId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"InvitationDocument")]
  public partial class InvitationDocument : global::ProtoBuf.IExtensible
  {
    public InvitationDocument() {}
    
    private Diadoc.Api.Proto.Events.SignedContent _SignedContent;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"SignedContent", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Diadoc.Api.Proto.Events.SignedContent SignedContent
    {
      get { return _SignedContent; }
      set { _SignedContent = value; }
    }
    private string _FileName;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"FileName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string FileName
    {
      get { return _FileName; }
      set { _FileName = value; }
    }

    private bool _SignatureRequested = (bool)false;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"SignatureRequested", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool SignatureRequested
    {
      get { return _SignatureRequested; }
      set { _SignatureRequested = value; }
    }

    private string _Type = "";
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"Type", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Type
    {
      get { return _Type; }
      set { _Type = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"AcquireCounteragentResult")]
  public partial class AcquireCounteragentResult : global::ProtoBuf.IExtensible
  {
    public AcquireCounteragentResult() {}
    
    private string _OrgId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"OrgId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string OrgId
    {
      get { return _OrgId; }
      set { _OrgId = value; }
    }

    private Diadoc.Api.Proto.DocumentId _InvitationDocumentId = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"InvitationDocumentId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.DocumentId InvitationDocumentId
    {
      get { return _InvitationDocumentId; }
      set { _InvitationDocumentId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"AcquireCounteragentResultV2")]
  public partial class AcquireCounteragentResultV2 : global::ProtoBuf.IExtensible
  {
    public AcquireCounteragentResultV2() {}
    
    private string _BoxId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"BoxId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string BoxId
    {
      get { return _BoxId; }
      set { _BoxId = value; }
    }

    private Diadoc.Api.Proto.DocumentId _InvitationDocumentId = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"InvitationDocumentId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.DocumentId InvitationDocumentId
    {
      get { return _InvitationDocumentId; }
      set { _InvitationDocumentId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}