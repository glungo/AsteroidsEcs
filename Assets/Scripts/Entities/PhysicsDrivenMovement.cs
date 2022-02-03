using Unity.Entities;

namespace Physics
{
    [GenerateAuthoringComponent]
    public struct PhysicsDrivenMovement : IComponentData
    {
        public float Speed;
        public float AngularSpeed;
        public float Drag;
        public float AngularDrag;
    }
}