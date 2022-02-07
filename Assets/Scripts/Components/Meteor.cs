using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct Meteor : IComponentData
    {
        public int SmallMeteorAmount;
        public Entity SmallMeteorPrefab;
    }
}