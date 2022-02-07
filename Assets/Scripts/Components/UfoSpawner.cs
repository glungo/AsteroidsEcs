using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct UfoSpawner : IComponentData
    {
        public float UfoSpawnCooldown;
        public int UfoPerRound;
        public Entity UfoPrefab;
    }
}