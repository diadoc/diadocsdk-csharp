//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: OuterDocflows/OuterDocflow.proto
// Note: requires additional types generated from: OuterDocflows/OuterDocflowStatus.proto
namespace Diadoc.Api.Proto.OuterDocflows
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"OuterDocflowInfo")]
  public partial class OuterDocflowInfo : global::ProtoBuf.IExtensible
  {
    public OuterDocflowInfo() {}
    
    private string _DocflowNamedId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"DocflowNamedId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string DocflowNamedId
    {
      get { return _DocflowNamedId; }
      set { _DocflowNamedId = value; }
    }
    private string _DocflowFriendlyName;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"DocflowFriendlyName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string DocflowFriendlyName
    {
      get { return _DocflowFriendlyName; }
      set { _DocflowFriendlyName = value; }
    }
    private Diadoc.Api.Proto.OuterDocflows.Status _Status;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"Status", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Diadoc.Api.Proto.OuterDocflows.Status Status
    {
      get { return _Status; }
      set { _Status = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}