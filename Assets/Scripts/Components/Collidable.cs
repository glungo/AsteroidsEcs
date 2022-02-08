using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct Collidable : IComponentData
    {
        public enum CollidableType {Default, Bullet, Player, Meteor, Pickup_Shield, Pickup_MultiBullet}

        public CollidableType Type;
        public bool Invulnerable;
    }
}