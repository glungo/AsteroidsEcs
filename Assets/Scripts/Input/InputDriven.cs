using Unity.Entities;

namespace Input
{
    public struct InputDriven : IComponentData
    {
        public float SpeedMultiplier;
        public float AngularSpeedMultiplier;
    }
}