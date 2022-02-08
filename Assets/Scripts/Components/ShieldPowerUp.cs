using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct ShieldPowerUp : IComponentData
    {
        public float Duration;
        public float RunningTime;
    }
}