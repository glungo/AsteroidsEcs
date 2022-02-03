using Unity.Entities;

namespace Entities
{
    [GenerateAuthoringComponent]
    public struct Shooter : IComponentData
    {
        public float FireCooldown;
        public Entity BulletPrefab;
    }
}