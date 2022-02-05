using Unity.Entities;

namespace Entities
{
    public struct MeteorSpawner : IComponentData
    {
        public float MeteorCooldown;
        public int MaxMeteors;
    }
}
