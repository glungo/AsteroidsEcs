using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct Collidable : IComponentData
    {
        public enum CollidableType {Bullet, Player, Meteor}

        public CollidableType Type;
    }
}