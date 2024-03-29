//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Docflow/DocflowApi.proto
// Note: requires additional types generated from: Timestamp.proto
// Note: requires additional types generated from: DocumentId.proto
// Note: requires additional types generated from: TotalCountType.proto
// Note: requires additional types generated from: TimeBasedFilter.proto
// Note: requires additional types generated from: Docflow/DocumentWithDocflow.proto
namespace Diadoc.Api.Proto.Docflow
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetDocflowBatchRequest")]
  public partial class GetDocflowBatchRequest : global::ProtoBuf.IExtensible
  {
    public GetDocflowBatchRequest() {}
    
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.GetDocflowRequest> _Requests = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.GetDocflowRequest>();
    [global::ProtoBuf.ProtoMember(1, Name=@"Requests", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.GetDocflowRequest> Requests
    {
      get { return _Requests; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetDocflowRequest")]
  public partial class GetDocflowRequest : global::ProtoBuf.IExtensible
  {
    public GetDocflowRequest() {}
    
    private Diadoc.Api.Proto.DocumentId _DocumentId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"DocumentId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Diadoc.Api.Proto.DocumentId DocumentId
    {
      get { return _DocumentId; }
      set { _DocumentId = value; }
    }

    private string _LastEventId = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"LastEventId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string LastEventId
    {
      get { return _LastEventId; }
      set { _LastEventId = value; }
    }

    private bool _InjectEntityContent = (bool)false;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"InjectEntityContent", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool InjectEntityContent
    {
      get { return _InjectEntityContent; }
      set { _InjectEntityContent = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetDocflowBatchResponse")]
  public partial class GetDocflowBatchResponse : global::ProtoBuf.IExtensible
  {
    public GetDocflowBatchResponse() {}
    
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocumentWithDocflow> _Documents = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocumentWithDocflow>();
    [global::ProtoBuf.ProtoMember(1, Name=@"Documents", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocumentWithDocflow> Documents
    {
      get { return _Documents; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SearchDocflowsRequest")]
  public partial class SearchDocflowsRequest : global::ProtoBuf.IExtensible
  {
    public SearchDocflowsRequest() {}
    
    private string _QueryString;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"QueryString", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string QueryString
    {
      get { return _QueryString; }
      set { _QueryString = value; }
    }

    private int _Count = (int)100;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Count", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)100)]
    public int Count
    {
      get { return _Count; }
      set { _Count = value; }
    }

    private int _FirstIndex = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"FirstIndex", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int FirstIndex
    {
      get { return _FirstIndex; }
      set { _FirstIndex = value; }
    }

    private Diadoc.Api.Proto.Docflow.SearchScope _Scope = Diadoc.Api.Proto.Docflow.SearchScope.SearchScopeAny;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"Scope", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(Diadoc.Api.Proto.Docflow.SearchScope.SearchScopeAny)]
    public Diadoc.Api.Proto.Docflow.SearchScope Scope
    {
      get { return _Scope; }
      set { _Scope = value; }
    }

    private bool _InjectEntityContent = (bool)false;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"InjectEntityContent", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool InjectEntityContent
    {
      get { return _InjectEntityContent; }
      set { _InjectEntityContent = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SearchDocflowsResponse")]
  public partial class SearchDocflowsResponse : global::ProtoBuf.IExtensible
  {
    public SearchDocflowsResponse() {}
    
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocumentWithDocflow> _Documents = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocumentWithDocflow>();
    [global::ProtoBuf.ProtoMember(1, Name=@"Documents", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocumentWithDocflow> Documents
    {
      get { return _Documents; }
    }
  

    private bool _HaveMoreDocuments = default(bool);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"HaveMoreDocuments", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool HaveMoreDocuments
    {
      get { return _HaveMoreDocuments; }
      set { _HaveMoreDocuments = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetDocflowsByPacketIdRequest")]
  public partial class GetDocflowsByPacketIdRequest : global::ProtoBuf.IExtensible
  {
    public GetDocflowsByPacketIdRequest() {}
    
    private string _PacketId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"PacketId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string PacketId
    {
      get { return _PacketId; }
      set { _PacketId = value; }
    }

    private int _Count = (int)100;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Count", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)100)]
    public int Count
    {
      get { return _Count; }
      set { _Count = value; }
    }

    private bool _InjectEntityContent = (bool)false;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"InjectEntityContent", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool InjectEntityContent
    {
      get { return _InjectEntityContent; }
      set { _InjectEntityContent = value; }
    }

    private byte[] _AfterIndexKey = null;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"AfterIndexKey", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] AfterIndexKey
    {
      get { return _AfterIndexKey; }
      set { _AfterIndexKey = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"FetchedDocument")]
  public partial class FetchedDocument : global::ProtoBuf.IExtensible
  {
    public FetchedDocument() {}
    
    private Diadoc.Api.Proto.Docflow.DocumentWithDocflow _Document;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"Document", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Diadoc.Api.Proto.Docflow.DocumentWithDocflow Document
    {
      get { return _Document; }
      set { _Document = value; }
    }
    private byte[] _IndexKey;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"IndexKey", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] IndexKey
    {
      get { return _IndexKey; }
      set { _IndexKey = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetDocflowsByPacketIdResponse")]
  public partial class GetDocflowsByPacketIdResponse : global::ProtoBuf.IExtensible
  {
    public GetDocflowsByPacketIdResponse() {}
    
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.FetchedDocument> _Documents = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.FetchedDocument>();
    [global::ProtoBuf.ProtoMember(1, Name=@"Documents", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.FetchedDocument> Documents
    {
      get { return _Documents; }
    }
  

    private byte[] _NextPageIndexKey = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"NextPageIndexKey", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] NextPageIndexKey
    {
      get { return _NextPageIndexKey; }
      set { _NextPageIndexKey = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetDocflowEventsRequest")]
  public partial class GetDocflowEventsRequest : global::ProtoBuf.IExtensible
  {
    public GetDocflowEventsRequest() {}
    
    private Diadoc.Api.Proto.TimeBasedFilter _Filter;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"Filter", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Diadoc.Api.Proto.TimeBasedFilter Filter
    {
      get { return _Filter; }
      set { _Filter = value; }
    }

    private byte[] _AfterIndexKey = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"AfterIndexKey", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] AfterIndexKey
    {
      get { return _AfterIndexKey; }
      set { _AfterIndexKey = value; }
    }

    private bool _PopulateDocuments = (bool)false;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"PopulateDocuments", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool PopulateDocuments
    {
      get { return _PopulateDocuments; }
      set { _PopulateDocuments = value; }
    }

    private bool _InjectEntityContent = (bool)false;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"InjectEntityContent", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool InjectEntityContent
    {
      get { return _InjectEntityContent; }
      set { _InjectEntityContent = value; }
    }

    private bool _PopulatePreviousDocumentStates = (bool)false;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"PopulatePreviousDocumentStates", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool PopulatePreviousDocumentStates
    {
      get { return _PopulatePreviousDocumentStates; }
      set { _PopulatePreviousDocumentStates = value; }
    }
    private readonly global::System.Collections.Generic.List<string> _MessageTypes = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(6, Name=@"MessageTypes", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> MessageTypes
    {
      get { return _MessageTypes; }
    }
  
    private readonly global::System.Collections.Generic.List<string> _DocumentDirections = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(7, Name=@"DocumentDirections", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> DocumentDirections
    {
      get { return _DocumentDirections; }
    }
  

    private string _DepartmentId = "";
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"DepartmentId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string DepartmentId
    {
      get { return _DepartmentId; }
      set { _DepartmentId = value; }
    }
    private readonly global::System.Collections.Generic.List<string> _TypeNamedIds = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(9, Name=@"TypeNamedIds", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> TypeNamedIds
    {
      get { return _TypeNamedIds; }
    }
  

    private string _CounteragentBoxId = "";
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"CounteragentBoxId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string CounteragentBoxId
    {
      get { return _CounteragentBoxId; }
      set { _CounteragentBoxId = value; }
    }

    private int _Limit = (int)100;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"Limit", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)100)]
    public int Limit
    {
      get { return _Limit; }
      set { _Limit = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetDocflowEventsResponse")]
  public partial class GetDocflowEventsResponse : global::ProtoBuf.IExtensible
  {
    public GetDocflowEventsResponse() {}
    

    private int _TotalCount = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"TotalCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int TotalCount
    {
      get { return _TotalCount; }
      set { _TotalCount = value; }
    }
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocflowEvent> _Events = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocflowEvent>();
    [global::ProtoBuf.ProtoMember(2, Name=@"Events", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocflowEvent> Events
    {
      get { return _Events; }
    }
  
    private Diadoc.Api.Proto.TotalCountType _TotalCountType;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"TotalCountType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public Diadoc.Api.Proto.TotalCountType TotalCountType
    {
      get { return _TotalCountType; }
      set { _TotalCountType = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"DocflowEvent")]
  public partial class DocflowEvent : global::ProtoBuf.IExtensible
  {
    public DocflowEvent() {}
    

    private string _EventId = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"EventId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string EventId
    {
      get { return _EventId; }
      set { _EventId = value; }
    }

    private Diadoc.Api.Proto.Timestamp _Timestamp = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Timestamp", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.Timestamp Timestamp
    {
      get { return _Timestamp; }
      set { _Timestamp = value; }
    }

    private Diadoc.Api.Proto.DocumentId _DocumentId = null;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"DocumentId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.DocumentId DocumentId
    {
      get { return _DocumentId; }
      set { _DocumentId = value; }
    }

    private byte[] _IndexKey = null;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"IndexKey", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] IndexKey
    {
      get { return _IndexKey; }
      set { _IndexKey = value; }
    }

    private Diadoc.Api.Proto.Docflow.DocumentWithDocflow _Document = null;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"Document", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.Docflow.DocumentWithDocflow Document
    {
      get { return _Document; }
      set { _Document = value; }
    }

    private string _PreviousEventId = "";
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"PreviousEventId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string PreviousEventId
    {
      get { return _PreviousEventId; }
      set { _PreviousEventId = value; }
    }

    private Diadoc.Api.Proto.Docflow.DocumentWithDocflow _PreviousDocumentState = null;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"PreviousDocumentState", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.Docflow.DocumentWithDocflow PreviousDocumentState
    {
      get { return _PreviousDocumentState; }
      set { _PreviousDocumentState = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"SearchScope")]
    public enum SearchScope
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SearchScopeAny", Value=0)]
      SearchScopeAny = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SearchScopeIncoming", Value=1)]
      SearchScopeIncoming = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SearchScopeOutgoing", Value=2)]
      SearchScopeOutgoing = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SearchScopeDeleted", Value=3)]
      SearchScopeDeleted = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SearchScopeInternal", Value=4)]
      SearchScopeInternal = 4
    }
  
}