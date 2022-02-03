using Unity.Entities;

namespace Physics
{
    [GenerateAuthoringComponent]
    public struct PhysicsDriven : IComponentData
    {
        public float Speed;
        public float AngularSpeed;
        public float Drag;
        public float AngularDrag;
    }
}