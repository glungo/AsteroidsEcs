using Unity.Entities;

namespace Entities.Utils
{
    public struct EntityBuffer : IBufferElementData
    {
        public Entity Element;
    }
}