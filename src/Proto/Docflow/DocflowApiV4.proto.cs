//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Docflow/DocflowApiV4.proto
// Note: requires additional types generated from: Timestamp.proto
// Note: requires additional types generated from: DocumentId.proto
// Note: requires additional types generated from: TotalCountType.proto
// Note: requires additional types generated from: Docflow/DocumentWithDocflowV4.proto
namespace Diadoc.Api.Proto.Docflow
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetDocflowBatchResponseV4")]
  public partial class GetDocflowBatchResponseV4 : global::ProtoBuf.IExtensible
  {
    public GetDocflowBatchResponseV4() {}
    
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocumentWithDocflowV4> _Documents = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocumentWithDocflowV4>();
    [global::ProtoBuf.ProtoMember(1, Name=@"Documents", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocumentWithDocflowV4> Documents
    {
      get { return _Documents; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SearchDocflowsResponseV4")]
  public partial class SearchDocflowsResponseV4 : global::ProtoBuf.IExtensible
  {
    public SearchDocflowsResponseV4() {}
    
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocumentWithDocflowV4> _Documents = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocumentWithDocflowV4>();
    [global::ProtoBuf.ProtoMember(1, Name=@"Documents", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocumentWithDocflowV4> Documents
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
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"FetchedDocumentV4")]
  public partial class FetchedDocumentV4 : global::ProtoBuf.IExtensible
  {
    public FetchedDocumentV4() {}
    
    private Diadoc.Api.Proto.Docflow.DocumentWithDocflowV4 _Document;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"Document", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Diadoc.Api.Proto.Docflow.DocumentWithDocflowV4 Document
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
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetDocflowsByPacketIdResponseV4")]
  public partial class GetDocflowsByPacketIdResponseV4 : global::ProtoBuf.IExtensible
  {
    public GetDocflowsByPacketIdResponseV4() {}
    
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.FetchedDocumentV4> _Documents = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.FetchedDocumentV4>();
    [global::ProtoBuf.ProtoMember(1, Name=@"Documents", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.FetchedDocumentV4> Documents
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
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetDocflowEventsResponseV4")]
  public partial class GetDocflowEventsResponseV4 : global::ProtoBuf.IExtensible
  {
    public GetDocflowEventsResponseV4() {}
    

    private int _TotalCount = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"TotalCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int TotalCount
    {
      get { return _TotalCount; }
      set { _TotalCount = value; }
    }
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocflowEventV4> _Events = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocflowEventV4>();
    [global::ProtoBuf.ProtoMember(2, Name=@"Events", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Docflow.DocflowEventV4> Events
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
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"DocflowEventV4")]
  public partial class DocflowEventV4 : global::ProtoBuf.IExtensible
  {
    public DocflowEventV4() {}
    

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

    private Diadoc.Api.Proto.Docflow.DocumentWithDocflowV4 _Document = null;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"Document", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.Docflow.DocumentWithDocflowV4 Document
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

    private Diadoc.Api.Proto.Docflow.DocumentWithDocflowV4 _PreviousDocumentState = null;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"PreviousDocumentState", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.Docflow.DocumentWithDocflowV4 PreviousDocumentState
    {
      get { return _PreviousDocumentState; }
      set { _PreviousDocumentState = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}