//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: RoamingOperator.proto
namespace Diadoc.Api.Proto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RoamingOperatorList")]
  public partial class RoamingOperatorList : global::ProtoBuf.IExtensible
  {
    public RoamingOperatorList() {}
    
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.RoamingOperator> _RoamingOperators = new global::System.Collections.Generic.List<Diadoc.Api.Proto.RoamingOperator>();
    [global::ProtoBuf.ProtoMember(1, Name=@"RoamingOperators", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.RoamingOperator> RoamingOperators
    {
      get { return _RoamingOperators; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RoamingOperator")]
  public partial class RoamingOperator : global::ProtoBuf.IExtensible
  {
    public RoamingOperator() {}
    
    private string _FnsId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"FnsId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string FnsId
    {
      get { return _FnsId; }
      set { _FnsId = value; }
    }
    private string _Name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"Name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string Name
    {
      get { return _Name; }
      set { _Name = value; }
    }
    private bool _IsActive;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"IsActive", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public bool IsActive
    {
      get { return _IsActive; }
      set { _IsActive = value; }
    }
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.OperatorFeature> _Features = new global::System.Collections.Generic.List<Diadoc.Api.Proto.OperatorFeature>();
    [global::ProtoBuf.ProtoMember(4, Name=@"Features", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.OperatorFeature> Features
    {
      get { return _Features; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"OperatorFeature")]
  public partial class OperatorFeature : global::ProtoBuf.IExtensible
  {
    public OperatorFeature() {}
    
    private string _Name;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"Name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string Name
    {
      get { return _Name; }
      set { _Name = value; }
    }
    private string _Description;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"Description", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string Description
    {
      get { return _Description; }
      set { _Description = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}