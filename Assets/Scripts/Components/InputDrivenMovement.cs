using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct InputDrivenMovement : IComponentData
    {
        public float SpeedMultiplier;
        public float AngularSpeedMultiplier;
    }
}