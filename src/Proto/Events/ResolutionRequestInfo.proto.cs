//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Events/ResolutionRequestInfo.proto
// Note: requires additional types generated from: ResolutionAction.proto
// Note: requires additional types generated from: ResolutionTarget.proto
// Note: requires additional types generated from: ResolutionRequestType.proto
namespace Diadoc.Api.Proto.Events
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ResolutionRequestInfo")]
  public partial class ResolutionRequestInfo : global::ProtoBuf.IExtensible
  {
    public ResolutionRequestInfo() {}
    

    private Diadoc.Api.Proto.ResolutionRequestType _RequestType = Diadoc.Api.Proto.ResolutionRequestType.UnknownResolutionRequestType;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"RequestType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(Diadoc.Api.Proto.ResolutionRequestType.UnknownResolutionRequestType)]
    public Diadoc.Api.Proto.ResolutionRequestType RequestType
    {
      get { return _RequestType; }
      set { _RequestType = value; }
    }
    private string _Author;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"Author", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string Author
    {
      get { return _Author; }
      set { _Author = value; }
    }

    private Diadoc.Api.Proto.ResolutionTarget _Target = null;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Target", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public Diadoc.Api.Proto.ResolutionTarget Target
    {
      get { return _Target; }
      set { _Target = value; }
    }

    private string _ResolvedWith = "";
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"ResolvedWith", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ResolvedWith
    {
      get { return _ResolvedWith; }
      set { _ResolvedWith = value; }
    }
    private readonly global::System.Collections.Generic.List<Diadoc.Api.Proto.ResolutionAction> _Actions = new global::System.Collections.Generic.List<Diadoc.Api.Proto.ResolutionAction>();
    [global::ProtoBuf.ProtoMember(5, Name=@"Actions", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<Diadoc.Api.Proto.ResolutionAction> Actions
    {
      get { return _Actions; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}