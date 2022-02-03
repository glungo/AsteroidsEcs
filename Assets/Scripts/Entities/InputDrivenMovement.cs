using Unity.Entities;

namespace Entities
{
    [GenerateAuthoringComponent]
    public struct InputDrivenMovement : IComponentData
    {
        public float SpeedMultiplier;
        public float AngularSpeedMultiplier;
    }
}