using QuesoStruct;
using System.Text;

namespace XMPlary
{
    public class XMFile : IDisposable
    {
        private Context context;
        private bool disposedValue;

        public XMFile(Stream stream)
        {
            context = new Context(stream, Endianess.Little, Encoding.ASCII);
        }

        public void WriteToOBJ(TextWriter writer)
        {
            context.Stream.Seek(0, SeekOrigin.Begin);

            var header = Serializers.Get<XMHeader>().Read(context);
            var body = new Context(header.Body.Stream, Endianess.Little, Encoding.ASCII);

            var vtxBuffer = FVFVertexBuffer.Factory
                .Create(header.FVFCode, header.VertexStride, header.VertexCount);
            vtxBuffer.WriteComponentText<D3D.FVF.XYZ>(writer, body);
            vtxBuffer.WriteComponentText<D3D.FVF.UV>(writer, body);

            // Only triangle lists are supported right now.

            var writeTexCoords = vtxBuffer.HasComponent<D3D.FVF.UV>();
            var reader = new BinaryReader(body.Stream);

            for(int i = 0; i < header.IndexCount; i += 3) 
            {
                var a = reader.ReadUInt16() + 1;
                var b = reader.ReadUInt16() + 1;
                var c = reader.ReadUInt16() + 1;

                if (writeTexCoords)
                    writer.WriteLine($"f {a}/{a} {b}/{b} {c}/{c}");
                else
                    writer.WriteLine($"f {a} {b} {c}");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Stream.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
