using QuesoStruct;

namespace XMPlary.D3D
{
    public interface IFVFComponent
    {
        int Order { get; }
        uint FVFMask { get; }

        ISerializer IO { get; }
    }

    public interface IFVFComponent<T> : IFVFComponent
        where T : IStructInstance, IOBJText
    {
        new ISerializer<T> IO { get; }
    }
}
