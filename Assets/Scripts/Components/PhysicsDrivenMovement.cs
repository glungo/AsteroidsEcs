using Unity.Entities;

namespace Components
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