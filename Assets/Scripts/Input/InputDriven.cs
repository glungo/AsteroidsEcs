using Unity.Entities;

namespace Input
{
    [GenerateAuthoringComponent]
    public struct InputDriven : IComponentData
    {
        public float SpeedMultiplier;
        public float AngularSpeedMultiplier;
    }
}