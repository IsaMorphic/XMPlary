using QuesoStruct;

namespace XMPlary.D3D
{
    public partial class FVF
    {
        [StructType]
        public partial class XYZ : IFVFComponent<XYZ>, IOBJText
        {
            public ISerializer<XYZ> IO => Serializers.Get<XYZ>();
            ISerializer IFVFComponent.IO => (ISerializer)IO;

            public int Order => 0;
            public uint FVFMask => 0x002;

            [StructMember]
            public float X { get; set; }

            [StructMember]
            public float Y { get; set; }

            [StructMember]
            public float Z { get; set; }

            public string ToOBJText() => $"v {X:0.} {Y:0.} {Z:0.}";
        }
    }
}
