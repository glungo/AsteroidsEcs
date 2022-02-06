using System;
using System.Collections.Generic;
using Entities;
using Unity.Entities;
using static Entities.Collidable;

namespace Systems
{
    public interface ICollisionHandler
    {
        void OnCollision(EntityCommandBuffer ecb, Entity e);
    }

    //default collision behaviour
    public class DestroyOnCollision : ICollisionHandler
    {
        public void OnCollision(EntityCommandBuffer ecb, Entity e)
        {
            ecb.AddComponent<DestroyTag>(e);
        }
    }

    public static class CollisionBehaviourFactory
    {
        private static readonly Dictionary<CollidableType, Type> SRegisteredTypes = new Dictionary<CollidableType, Type>()
        {
            {CollidableType.Meteor, typeof(DestroyOnCollision)},
            {CollidableType.Bullet, typeof(DestroyOnCollision)},
            {CollidableType.Player, typeof(DestroyOnCollision)}
        };
        public static ICollisionHandler GetCollisionBehaviour(CollidableType t)
        {
            var collisionBehaviour = 
                SRegisteredTypes.ContainsKey(t) ? SRegisteredTypes[t] : typeof(DestroyOnCollision);
            return Activator.CreateInstance(collisionBehaviour, true) as ICollisionHandler;
        }
    }
}