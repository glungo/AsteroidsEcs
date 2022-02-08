using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct MultiBulletPowerUp : IComponentData
    {
        public float Duration;
        public float RunningTime;
    }
}