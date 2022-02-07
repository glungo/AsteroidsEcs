using Unity.Entities;

namespace Components
{
    public struct MeteorSpawner : IComponentData
    {
        public float MeteorCooldown;
        public int MaxMeteors;
    }
}
