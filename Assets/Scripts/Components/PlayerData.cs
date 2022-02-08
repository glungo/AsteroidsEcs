using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct PlayerData : IComponentData
    {
        public Entity PlayerPrefab;
        public float3 PlayerStartingPos;
    }
}