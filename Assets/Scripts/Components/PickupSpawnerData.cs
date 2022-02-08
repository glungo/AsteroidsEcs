using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct PickupSpawnerData : IComponentData
    {
        public float Cooldown;
        public int PickupsPerRound;
        public Entity _shieldPrefab;
        public Entity _multiBulletPrefab;
    }
}