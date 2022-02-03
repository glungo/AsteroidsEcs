using Unity.Entities;

namespace Physics
{
    public struct PhysicsDriven : IComponentData
    {
        public float Speed; //acceleration is only in the forward direction
        public float AngularSpeed;
        public float Accel;
        public float AngularAccel;
    }
}