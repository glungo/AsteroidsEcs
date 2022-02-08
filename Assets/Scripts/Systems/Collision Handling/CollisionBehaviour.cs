using System;
using System.Collections.Generic;
using Components;
using Unity.Entities;
using static Components.Collidable;

namespace Systems.Collision_Handling
{
    public interface ICollisionHandler
    {
        void OnCollision(EntityCommandBuffer ecb, Entity e);
    }

    //default collision behaviour
    public struct DestroyOnCollision : ICollisionHandler
    {
        public void OnCollision(EntityCommandBuffer ecb, Entity e)
        {
            ecb.AddComponent<DestroyTag>(e);
        }
    }
    
    public struct SpawnMeteorsOnCollision : ICollisionHandler
    {
        public void OnCollision(EntityCommandBuffer ecb, Entity e)
        {
            ecb.AddComponent<SpawnSmallMeteorsTag>(e);
        }
    }

    public struct QueryForRespawnOnCollision : ICollisionHandler
    {
        public void OnCollision(EntityCommandBuffer ecb, Entity e)
        {
            ecb.DestroyEntity(e);
            var requestRespawnEntity = ecb.CreateEntity();
            ecb.AddComponent<RequestRespawn>(requestRespawnEntity);
        }
    }

    public static class CollisionBehaviourFactory
    {
        private static readonly Dictionary<CollidableType, Type> SRegisteredTypes = new Dictionary<CollidableType, Type>()
        {
            {CollidableType.Meteor, typeof(SpawnMeteorsOnCollision)},
            {CollidableType.Bullet, typeof(DestroyOnCollision)},
            {CollidableType.Player, typeof(QueryForRespawnOnCollision)}
        };
        public static ICollisionHandler GetCollisionBehaviour(CollidableType t)
        {
            var collisionBehaviour = 
                SRegisteredTypes.ContainsKey(t) ? SRegisteredTypes[t] : typeof(DestroyOnCollision);
            return Activator.CreateInstance(collisionBehaviour) as ICollisionHandler;
        }
    }
}