using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct BoundsBehaviour : IComponentData
    {
        public bool WrapOnBound;
    }
}
