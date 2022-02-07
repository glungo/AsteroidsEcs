using Unity.Entities;

namespace Components.Utils
{
    public struct EntityBuffer : IBufferElementData
    {
        public Entity Element;
    }
}