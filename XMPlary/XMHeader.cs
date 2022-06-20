using QuesoStruct;
using QuesoStruct.Types.Primitives;

namespace XMPlary
{
    [StructType]
    public partial class XMHeader : IBytesOwner
    {
        [StructMember]
        public uint PrimitiveType { get; set; }

        [StructMember]
        public uint FaceCount { get; set; }

        [StructMember]
        public uint FVFCode { get; set; }

        [StructMember]
        public uint VertexStride { get; set; }

        [StructMember]
        public uint VertexCount { get; set; }

        [StructMember]
        public uint IndexCount { get; set; }

        [StructMember]
        [AutoInitialize]
        public Bytes Body { get; set; }

        public long BytesLength => VertexStride * VertexCount + IndexCount * sizeof(ushort);
    }
}
