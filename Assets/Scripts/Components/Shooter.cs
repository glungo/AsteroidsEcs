using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct Shooter : IComponentData
    {
        public float FireCooldown;
        public Entity BulletPrefab;
    }
}