using QuesoStruct;

namespace XMPlary.D3D
{
    public partial class FVF
    {
        [StructType]
        public partial class UV : IFVFComponent<UV>, IOBJText
        {
            public ISerializer<UV> IO => Serializers.Get<UV>();
            ISerializer IFVFComponent.IO => (ISerializer)IO;

            public int Order => 1;

            // Currently disabled for testing
            public uint FVFMask => 0xF00; //0x100;

            [StructMember]
            public float U { get; set; }

            [StructMember]
            public float V { get; set; }

            public string ToOBJText() => $"vt {U:0.} {V:0.}";
        }
    }
}
