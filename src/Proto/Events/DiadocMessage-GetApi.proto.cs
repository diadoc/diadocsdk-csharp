//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Events/DiadocMessage-GetApi.proto
// Note: requires additional types generated from: Content.proto
// Note: requires additional types generated from: DocumentId.proto
// Note: requires additional types generated from: Documents/Document.proto
// Note: requires additional types generated from: Events/ResolutionInfo.proto
// Note: requires additional types generated from: Events/ResolutionRequestInfo.proto
// Note: requires additional types generated from: Events/ResolutionRequestDenialInfo.proto
namespace Diadoc.Api.Proto.Events
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"BoxEventList")]
  public partial class BoxEventList : global::ProtoBuf.IExtensible
  {
    public BoxEventList() {}
    
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Events.BoxEvent> _Events = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Events.BoxEvent>();
    [global::ProtoBuf.ProtoMember(1, Name=@"Events", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Events.BoxEvent> Events
    {
      get { return _Events; }
    }
  

    private int _TotalCount = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"TotalCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int TotalCount
    {
      get { return _TotalCount; }
      set { _TotalCount = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"BoxEvent")]
  public partial class BoxEvent : global::ProtoBuf.IExtensible
  {
    public BoxEvent() {}
    
    private string _EventId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"EventId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string EventId
    {
      get { return _EventId; }
      set { _EventId = value; }
    }

    private Diadoc.Api.Proto.Events.Message _Message = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Message", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.Events.Message Message
    {
      get { return _Message; }
      set { _Message = value; }
    }

    private Diadoc.Api.Proto.Events.MessagePatch _Patch = null;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Patch", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.Events.MessagePatch Patch
    {
      get { return _Patch; }
      set { _Patch = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Message")]
  public partial class Message : global::ProtoBuf.IExtensible
  {
    public Message() {}
    
    private string _MessageId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"MessageId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string MessageId
    {
      get { return _MessageId; }
      set { _MessageId = value; }
    }
    private long _TimestampTicks;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"TimestampTicks", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public long TimestampTicks
    {
      get { return _TimestampTicks; }
      set { _TimestampTicks = value; }
    }
    private long _LastPatchTimestampTicks;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"LastPatchTimestampTicks", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public long LastPatchTimestampTicks
    {
      get { return _LastPatchTimestampTicks; }
      set { _LastPatchTimestampTicks = value; }
    }
    private string _FromBoxId;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"FromBoxId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string FromBoxId
    {
      get { return _FromBoxId; }
      set { _FromBoxId = value; }
    }
    private string _FromTitle;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"FromTitle", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string FromTitle
    {
      get { return _FromTitle; }
      set { _FromTitle = value; }
    }

    private string _ToBoxId = "";
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"ToBoxId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ToBoxId
    {
      get { return _ToBoxId; }
      set { _ToBoxId = value; }
    }

    private string _ToTitle = "";
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"ToTitle", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ToTitle
    {
      get { return _ToTitle; }
      set { _ToTitle = value; }
    }
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Events.Entity> _Entities = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Events.Entity>();
    [global::ProtoBuf.ProtoMember(8, Name=@"Entities", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Events.Entity> Entities
    {
      get { return _Entities; }
    }
  

    private bool _IsDraft = (bool)false;
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"IsDraft", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool IsDraft
    {
      get { return _IsDraft; }
      set { _IsDraft = value; }
    }

    private bool _DraftIsLocked = (bool)false;
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"DraftIsLocked", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool DraftIsLocked
    {
      get { return _DraftIsLocked; }
      set { _DraftIsLocked = value; }
    }

    private bool _DraftIsRecycled = (bool)false;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"DraftIsRecycled", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool DraftIsRecycled
    {
      get { return _DraftIsRecycled; }
      set { _DraftIsRecycled = value; }
    }

    private string _CreatedFromDraftId = "";
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"CreatedFromDraftId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string CreatedFromDraftId
    {
      get { return _CreatedFromDraftId; }
      set { _CreatedFromDraftId = value; }
    }
    private readonly global::System.Collections.Generic.List<string> _DraftIsTransformedToMessageIdList = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(13, Name=@"DraftIsTransformedToMessageIdList", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> DraftIsTransformedToMessageIdList
    {
      get { return _DraftIsTransformedToMessageIdList; }
    }
  

    private bool _IsDeleted = (bool)false;
    [global::ProtoBuf.ProtoMember(14, IsRequired = false, Name=@"IsDeleted", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool IsDeleted
    {
      get { return _IsDeleted; }
      set { _IsDeleted = value; }
    }

    private bool _IsTest = (bool)false;
    [global::ProtoBuf.ProtoMember(15, IsRequired = false, Name=@"IsTest", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool IsTest
    {
      get { return _IsTest; }
      set { _IsTest = value; }
    }

    private bool _IsInternal = (bool)false;
    [global::ProtoBuf.ProtoMember(16, IsRequired = false, Name=@"IsInternal", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool IsInternal
    {
      get { return _IsInternal; }
      set { _IsInternal = value; }
    }

    private bool _IsProxified = (bool)false;
    [global::ProtoBuf.ProtoMember(17, IsRequired = false, Name=@"IsProxified", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool IsProxified
    {
      get { return _IsProxified; }
      set { _IsProxified = value; }
    }

    private string _ProxyBoxId = "";
    [global::ProtoBuf.ProtoMember(18, IsRequired = false, Name=@"ProxyBoxId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ProxyBoxId
    {
      get { return _ProxyBoxId; }
      set { _ProxyBoxId = value; }
    }

    private string _ProxyTitle = "";
    [global::ProtoBuf.ProtoMember(19, IsRequired = false, Name=@"ProxyTitle", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ProxyTitle
    {
      get { return _ProxyTitle; }
      set { _ProxyTitle = value; }
    }

    private bool _PacketIsLocked = (bool)false;
    [global::ProtoBuf.ProtoMember(20, IsRequired = false, Name=@"PacketIsLocked", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool PacketIsLocked
    {
      get { return _PacketIsLocked; }
      set { _PacketIsLocked = value; }
    }
    private string _FromDepartmentId;
    [global::ProtoBuf.ProtoMember(21, IsRequired = true, Name=@"FromDepartmentId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string FromDepartmentId
    {
      get { return _FromDepartmentId; }
      set { _FromDepartmentId = value; }
    }

    private string _ToDepartmentId = "";
    [global::ProtoBuf.ProtoMember(22, IsRequired = false, Name=@"ToDepartmentId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ToDepartmentId
    {
      get { return _ToDepartmentId; }
      set { _ToDepartmentId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MessagePatch")]
  public partial class MessagePatch : global::ProtoBuf.IExtensible
  {
    public MessagePatch() {}
    
    private string _MessageId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"MessageId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string MessageId
    {
      get { return _MessageId; }
      set { _MessageId = value; }
    }
    private long _TimestampTicks;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"TimestampTicks", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    public long TimestampTicks
    {
      get { return _TimestampTicks; }
      set { _TimestampTicks = value; }
    }
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Events.Entity> _Entities = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Events.Entity>();
    [global::ProtoBuf.ProtoMember(3, Name=@"Entities", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Events.Entity> Entities
    {
      get { return _Entities; }
    }
  

    private bool _ForDraft = (bool)false;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"ForDraft", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool ForDraft
    {
      get { return _ForDraft; }
      set { _ForDraft = value; }
    }

    private bool _DraftIsRecycled = (bool)false;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"DraftIsRecycled", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool DraftIsRecycled
    {
      get { return _DraftIsRecycled; }
      set { _DraftIsRecycled = value; }
    }
    private readonly global::System.Collections.Generic.List<string> _DraftIsTransformedToMessageIdList = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(6, Name=@"DraftIsTransformedToMessageIdList", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> DraftIsTransformedToMessageIdList
    {
      get { return _DraftIsTransformedToMessageIdList; }
    }
  

    private bool _DraftIsLocked = (bool)false;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"DraftIsLocked", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool DraftIsLocked
    {
      get { return _DraftIsLocked; }
      set { _DraftIsLocked = value; }
    }

    private bool _MessageIsDeleted = (bool)false;
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"MessageIsDeleted", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool MessageIsDeleted
    {
      get { return _MessageIsDeleted; }
      set { _MessageIsDeleted = value; }
    }
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Events.EntityPatch> _EntityPatches = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Events.EntityPatch>();
    [global::ProtoBuf.ProtoMember(9, Name=@"EntityPatches", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Events.EntityPatch> EntityPatches
    {
      get { return _EntityPatches; }
    }
  

    private bool _MessageIsRestored = (bool)false;
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"MessageIsRestored", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool MessageIsRestored
    {
      get { return _MessageIsRestored; }
      set { _MessageIsRestored = value; }
    }

    private bool _MessageIsDelivered = (bool)false;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"MessageIsDelivered", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool MessageIsDelivered
    {
      get { return _MessageIsDelivered; }
      set { _MessageIsDelivered = value; }
    }

    private string _DeliveredPatchId = "";
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"DeliveredPatchId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string DeliveredPatchId
    {
      get { return _DeliveredPatchId; }
      set { _DeliveredPatchId = value; }
    }
    private string _PatchId;
    [global::ProtoBuf.ProtoMember(13, IsRequired = true, Name=@"PatchId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string PatchId
    {
      get { return _PatchId; }
      set { _PatchId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Entity")]
  public partial class Entity : global::ProtoBuf.IExtensible
  {
    public Entity() {}
    

    private Diadoc.Api.Proto.Events.EntityType _EntityType = Diadoc.Api.Proto.Events.EntityType.UnknownEntityType;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"EntityType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(Diadoc.Api.Proto.Events.EntityType.UnknownEntityType)]
    public Diadoc.Api.Proto.Events.EntityType EntityType
    {
      get { return _EntityType; }
      set { _EntityType = value; }
    }
    private string _EntityId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"EntityId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string EntityId
    {
      get { return _EntityId; }
      set { _EntityId = value; }
    }

    private string _ParentEntityId = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"ParentEntityId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ParentEntityId
    {
      get { return _ParentEntityId; }
      set { _ParentEntityId = value; }
    }

    private Diadoc.Api.Proto.Content _Content = null;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"Content", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.Content Content
    {
      get { return _Content; }
      set { _Content = value; }
    }

    private Diadoc.Api.Proto.Events.AttachmentType _AttachmentType = Diadoc.Api.Proto.Events.AttachmentType.UnknownAttachmentType;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"AttachmentType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(Diadoc.Api.Proto.Events.AttachmentType.UnknownAttachmentType)]
    public Diadoc.Api.Proto.Events.AttachmentType AttachmentType
    {
      get { return _AttachmentType; }
      set { _AttachmentType = value; }
    }

    private string _FileName = "";
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"FileName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string FileName
    {
      get { return _FileName; }
      set { _FileName = value; }
    }

    private bool _NeedRecipientSignature = (bool)false;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"NeedRecipientSignature", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool NeedRecipientSignature
    {
      get { return _NeedRecipientSignature; }
      set { _NeedRecipientSignature = value; }
    }

    private string _SignerBoxId = "";
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"SignerBoxId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string SignerBoxId
    {
      get { return _SignerBoxId; }
      set { _SignerBoxId = value; }
    }

    private string _NotDeliveredEventId = "";
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"NotDeliveredEventId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string NotDeliveredEventId
    {
      get { return _NotDeliveredEventId; }
      set { _NotDeliveredEventId = value; }
    }

    private Diadoc.Api.Proto.Documents.Document _DocumentInfo = null;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"DocumentInfo", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.Documents.Document DocumentInfo
    {
      get { return _DocumentInfo; }
      set { _DocumentInfo = value; }
    }

    private long _RawCreationDate = (long)0;
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"RawCreationDate", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((long)0)]
    public long RawCreationDate
    {
      get { return _RawCreationDate; }
      set { _RawCreationDate = value; }
    }

    private Diadoc.Api.Proto.Events.ResolutionInfo _ResolutionInfo = null;
    [global::ProtoBuf.ProtoMember(13, IsRequired = false, Name=@"ResolutionInfo", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.Events.ResolutionInfo ResolutionInfo
    {
      get { return _ResolutionInfo; }
      set { _ResolutionInfo = value; }
    }

    private string _SignerDepartmentId = "";
    [global::ProtoBuf.ProtoMember(14, IsRequired = false, Name=@"SignerDepartmentId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string SignerDepartmentId
    {
      get { return _SignerDepartmentId; }
      set { _SignerDepartmentId = value; }
    }

    private Diadoc.Api.Proto.Events.ResolutionRequestInfo _ResolutionRequestInfo = null;
    [global::ProtoBuf.ProtoMember(15, IsRequired = false, Name=@"ResolutionRequestInfo", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.Events.ResolutionRequestInfo ResolutionRequestInfo
    {
      get { return _ResolutionRequestInfo; }
      set { _ResolutionRequestInfo = value; }
    }

    private Diadoc.Api.Proto.Events.ResolutionRequestDenialInfo _ResolutionRequestDenialInfo = null;
    [global::ProtoBuf.ProtoMember(16, IsRequired = false, Name=@"ResolutionRequestDenialInfo", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.Events.ResolutionRequestDenialInfo ResolutionRequestDenialInfo
    {
      get { return _ResolutionRequestDenialInfo; }
      set { _ResolutionRequestDenialInfo = value; }
    }

    private bool _NeedReceipt = (bool)false;
    [global::ProtoBuf.ProtoMember(17, IsRequired = false, Name=@"NeedReceipt", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool NeedReceipt
    {
      get { return _NeedReceipt; }
      set { _NeedReceipt = value; }
    }

    private string _PacketId = "";
    [global::ProtoBuf.ProtoMember(18, IsRequired = false, Name=@"PacketId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string PacketId
    {
      get { return _PacketId; }
      set { _PacketId = value; }
    }

    private bool _IsApprovementSignature = (bool)false;
    [global::ProtoBuf.ProtoMember(19, IsRequired = false, Name=@"IsApprovementSignature", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool IsApprovementSignature
    {
      get { return _IsApprovementSignature; }
      set { _IsApprovementSignature = value; }
    }

    private bool _IsEncryptedContent = (bool)false;
    [global::ProtoBuf.ProtoMember(20, IsRequired = false, Name=@"IsEncryptedContent", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool IsEncryptedContent
    {
      get { return _IsEncryptedContent; }
      set { _IsEncryptedContent = value; }
    }

    private string _AttachmentFormat = "";
    [global::ProtoBuf.ProtoMember(21, IsRequired = false, Name=@"AttachmentFormat", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string AttachmentFormat
    {
      get { return _AttachmentFormat; }
      set { _AttachmentFormat = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"EntityPatch")]
  public partial class EntityPatch : global::ProtoBuf.IExtensible
  {
    public EntityPatch() {}
    
    private string _EntityId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"EntityId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string EntityId
    {
      get { return _EntityId; }
      set { _EntityId = value; }
    }

    private bool _DocumentIsDeleted = (bool)false;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"DocumentIsDeleted", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool DocumentIsDeleted
    {
      get { return _DocumentIsDeleted; }
      set { _DocumentIsDeleted = value; }
    }

    private string _MovedToDepartment = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"MovedToDepartment", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string MovedToDepartment
    {
      get { return _MovedToDepartment; }
      set { _MovedToDepartment = value; }
    }

    private bool _DocumentIsRestored = (bool)false;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"DocumentIsRestored", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool DocumentIsRestored
    {
      get { return _DocumentIsRestored; }
      set { _DocumentIsRestored = value; }
    }

    private bool _ContentIsPatched = (bool)false;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"ContentIsPatched", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool ContentIsPatched
    {
      get { return _ContentIsPatched; }
      set { _ContentIsPatched = value; }
    }

    private string _ForwardedToBoxId = "";
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"ForwardedToBoxId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ForwardedToBoxId
    {
      get { return _ForwardedToBoxId; }
      set { _ForwardedToBoxId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"EntityType")]
    public enum EntityType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"UnknownEntityType", Value=0)]
      UnknownEntityType = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Attachment", Value=1)]
      Attachment = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Signature", Value=2)]
      Signature = 2
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"AttachmentType")]
    public enum AttachmentType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"UnknownAttachmentType", Value=-1)]
      UnknownAttachmentType = -1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Nonformalized", Value=0)]
      Nonformalized = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Invoice", Value=1)]
      Invoice = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"InvoiceReceipt", Value=2)]
      InvoiceReceipt = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"InvoiceConfirmation", Value=3)]
      InvoiceConfirmation = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"InvoiceCorrectionRequest", Value=4)]
      InvoiceCorrectionRequest = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"AttachmentComment", Value=5)]
      AttachmentComment = 5,
            
      [global::ProtoBuf.ProtoEnum(Name=@"DeliveryFailureNotification", Value=6)]
      DeliveryFailureNotification = 6,
            
      [global::ProtoBuf.ProtoEnum(Name=@"EancomInvoic", Value=7)]
      EancomInvoic = 7,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SignatureRequestRejection", Value=8)]
      SignatureRequestRejection = 8,
            
      [global::ProtoBuf.ProtoEnum(Name=@"EcrCatConformanceCertificateMetadata", Value=9)]
      EcrCatConformanceCertificateMetadata = 9,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SignatureVerificationReport", Value=10)]
      SignatureVerificationReport = 10,
            
      [global::ProtoBuf.ProtoEnum(Name=@"TrustConnectionRequest", Value=11)]
      TrustConnectionRequest = 11,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Torg12", Value=12)]
      Torg12 = 12,
            
      [global::ProtoBuf.ProtoEnum(Name=@"InvoiceRevision", Value=13)]
      InvoiceRevision = 13,
            
      [global::ProtoBuf.ProtoEnum(Name=@"InvoiceCorrection", Value=14)]
      InvoiceCorrection = 14,
            
      [global::ProtoBuf.ProtoEnum(Name=@"InvoiceCorrectionRevision", Value=15)]
      InvoiceCorrectionRevision = 15,
            
      [global::ProtoBuf.ProtoEnum(Name=@"AcceptanceCertificate", Value=16)]
      AcceptanceCertificate = 16,
            
      [global::ProtoBuf.ProtoEnum(Name=@"StructuredData", Value=17)]
      StructuredData = 17,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ProformaInvoice", Value=18)]
      ProformaInvoice = 18,
            
      [global::ProtoBuf.ProtoEnum(Name=@"XmlTorg12", Value=19)]
      XmlTorg12 = 19,
            
      [global::ProtoBuf.ProtoEnum(Name=@"XmlAcceptanceCertificate", Value=20)]
      XmlAcceptanceCertificate = 20,
            
      [global::ProtoBuf.ProtoEnum(Name=@"XmlTorg12BuyerTitle", Value=21)]
      XmlTorg12BuyerTitle = 21,
            
      [global::ProtoBuf.ProtoEnum(Name=@"XmlAcceptanceCertificateBuyerTitle", Value=22)]
      XmlAcceptanceCertificateBuyerTitle = 22,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Resolution", Value=23)]
      Resolution = 23,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ResolutionRequest", Value=24)]
      ResolutionRequest = 24,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ResolutionRequestDenial", Value=25)]
      ResolutionRequestDenial = 25,
            
      [global::ProtoBuf.ProtoEnum(Name=@"PriceList", Value=26)]
      PriceList = 26,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Receipt", Value=27)]
      Receipt = 27,
            
      [global::ProtoBuf.ProtoEnum(Name=@"XmlSignatureRejection", Value=28)]
      XmlSignatureRejection = 28,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RevocationRequest", Value=29)]
      RevocationRequest = 29,
            
      [global::ProtoBuf.ProtoEnum(Name=@"PriceListAgreement", Value=30)]
      PriceListAgreement = 30,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CertificateRegistry", Value=34)]
      CertificateRegistry = 34,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ReconciliationAct", Value=35)]
      ReconciliationAct = 35,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Contract", Value=36)]
      Contract = 36,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Torg13", Value=37)]
      Torg13 = 37,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ServiceDetails", Value=38)]
      ServiceDetails = 38,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RoamingNotification", Value=39)]
      RoamingNotification = 39,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SupplementaryAgreement", Value=40)]
      SupplementaryAgreement = 40,
            
      [global::ProtoBuf.ProtoEnum(Name=@"UniversalTransferDocument", Value=41)]
      UniversalTransferDocument = 41,
            
      [global::ProtoBuf.ProtoEnum(Name=@"UniversalTransferDocumentBuyerTitle", Value=42)]
      UniversalTransferDocumentBuyerTitle = 42,
            
      [global::ProtoBuf.ProtoEnum(Name=@"UniversalTransferDocumentRevision", Value=45)]
      UniversalTransferDocumentRevision = 45,
            
      [global::ProtoBuf.ProtoEnum(Name=@"UniversalCorrectionDocument", Value=49)]
      UniversalCorrectionDocument = 49,
            
      [global::ProtoBuf.ProtoEnum(Name=@"UniversalCorrectionDocumentRevision", Value=50)]
      UniversalCorrectionDocumentRevision = 50,
            
      [global::ProtoBuf.ProtoEnum(Name=@"UniversalCorrectionDocumentBuyerTitle", Value=51)]
      UniversalCorrectionDocumentBuyerTitle = 51,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CustomData", Value=64)]
      CustomData = 64,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MoveDocument", Value=65)]
      MoveDocument = 65,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ResolutionChainAssignmentAttachment", Value=66)]
      ResolutionChainAssignmentAttachment = 66
    }
  
}