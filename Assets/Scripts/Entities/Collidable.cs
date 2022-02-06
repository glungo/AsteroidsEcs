using Unity.Entities;

namespace Entities
{
    [GenerateAuthoringComponent]
    public struct Collidable : IComponentData
    {
        public enum CollidableType {Bullet, Player, Meteor}

        public CollidableType Type;
    }
}