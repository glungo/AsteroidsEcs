using Unity.Entities;

namespace Entities
{
    [GenerateAuthoringComponent]
    public struct BoundsBehaviour : IComponentData
    {
        public bool WrapOnBound;
    }
}
