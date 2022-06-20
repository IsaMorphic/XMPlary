
using QuesoStruct;
using System.Text;
using XMPlary.D3D;

namespace XMPlary
{
    public class FVFVertexBuffer
    {
        public class Factory
        {
            /*
                https://narkive.com/E9wd6F1n:3.379.191
                For reference the order is:

                1. Position
                2. RHW
                3. Blending weights
                4. Vertex Normal
                5. Vertex point size
                6. Diffuse colour
                7. Specular colour
                8. Texture co-ords
            */

            public static readonly IFVFComponent[] FVFs = new IFVFComponent[] { new FVF.XYZ(), new FVF.UV() };

            public static FVFVertexBuffer Create(uint fvfCode, uint vertexStride, uint vertexCount)
            {
                return new FVFVertexBuffer(FVFs
                    .Where(comp => (comp.FVFMask & fvfCode) == comp.FVFMask)
                    .OrderBy(comp => comp.Order)
                    .ToArray(), vertexStride, vertexCount);
            }
        }

        private IFVFComponent[] decl;
        private uint vertexStride;
        private uint vertexCount;

        internal FVFVertexBuffer(IFVFComponent[] decl, uint vertexStride, uint vertexCount)
        {
            this.decl = decl;
            this.vertexStride = vertexStride;
            this.vertexCount = vertexCount;
        }

        public bool HasComponent<T>()
            where T : IStructInstance, IOBJText
        {
            return decl.Any(comp => comp is IFVFComponent<T>);
        }

        public void WriteComponentText<T>(TextWriter writer, Context context)
            where T : IStructInstance, IOBJText
        {
            int i;
            for (i = 0; i - 1 < vertexCount; i++)
            {
                context.Stream.Seek(i * vertexStride, SeekOrigin.Begin);
                if (i == vertexCount) break;

                foreach (var comp in decl)
                {
                    if (comp is IFVFComponent<T> spec)
                    {
                        writer.WriteLine(spec.IO.Read(context).ToOBJText());
                    }
                    else
                    {
                        comp.IO.Read(context);
                    }
                }
            }
        }
    }
}
