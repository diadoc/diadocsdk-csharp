//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Documents/DocumentList.proto
// Note: requires additional types generated from: Documents/Document.proto
namespace Diadoc.Api.Proto.Documents
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"DocumentList")]
  public partial class DocumentList : global::ProtoBuf.IExtensible
  {
    public DocumentList() {}
    
    private int _TotalCount;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"TotalCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int TotalCount
    {
      get { return _TotalCount; }
      set { _TotalCount = value; }
    }
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.Documents.Document> _Documents = new global::System.Collections.Generic.List<Diadoc.Api.Proto.Documents.Document>();
    [global::ProtoBuf.ProtoMember(2, Name=@"Documents", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.Documents.Document> Documents
    {
      get { return _Documents; }
    }
  

    private bool _HasMoreResults = default(bool);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"HasMoreResults", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool HasMoreResults
    {
      get { return _HasMoreResults; }
      set { _HasMoreResults = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}