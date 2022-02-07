using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct Player : IComponentData
    {
        public float FireCooldown;
        public Entity BulletPrefab;
    }
}