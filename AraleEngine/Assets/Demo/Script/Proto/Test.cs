//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Proto/test.proto
namespace Proto.test
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TestProto")]
  public partial class TestProto : global::ProtoBuf.IExtensible
  {
    public TestProto() {}
    
    private string _a;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"a", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string a
    {
      get { return _a; }
      set { _a = value; }
    }

    private int _b = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"b", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int b
    {
      get { return _b; }
      set { _b = value; }
    }

    private int _c = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"c", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int c
    {
      get { return _c; }
      set { _c = value; }
    }
    private readonly global::System.Collections.Generic.List<int> _d = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(4, Name=@"d", DataFormat = global::ProtoBuf.DataFormat.TwosComplement, Options = global::ProtoBuf.MemberSerializationOptions.Packed)]
    public global::System.Collections.Generic.List<int> d
    {
      get { return _d; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}