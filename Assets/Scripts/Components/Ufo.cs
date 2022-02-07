
using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct Ufo : IComponentData
    {
        public float ElapsedTimeSinceLastShot;
        public float FireCooldown;
        public Entity BulletPrefab;
    }
}
