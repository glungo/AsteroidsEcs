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

    public struct AddShieldOnCollision : ICollisionHandler
    {
        public void OnCollision(EntityCommandBuffer ecb, Entity e)
        {
            ecb.AddComponent(e, new ShieldPowerUp{Duration = 1.5f, RunningTime = 0});
        }
    }
    
    public struct AddMultiBulletOnCollision : ICollisionHandler
    {
        public void OnCollision(EntityCommandBuffer ecb, Entity e)
        {
            ecb.AddComponent(e, new MultiBulletPowerUp{Duration = 1f, RunningTime = 0});
        }
    }

    public static class CollisionBehaviourFactory
    {
        private static readonly Dictionary<Tuple<CollidableType, CollidableType>, Type> SRegisteredTypes =
            new Dictionary<Tuple<CollidableType, CollidableType>, Type>()
            {
                {
                    new Tuple<CollidableType, CollidableType>(CollidableType.Meteor, CollidableType.Bullet),
                    typeof(SpawnMeteorsOnCollision)
                },
                {
                    new Tuple<CollidableType, CollidableType>(CollidableType.Meteor, CollidableType.Player),
                    typeof(SpawnMeteorsOnCollision)
                },
                {
                    new Tuple<CollidableType, CollidableType>(CollidableType.Player, CollidableType.Pickup_Shield),
                    typeof(AddShieldOnCollision)
                },
                {
                    new Tuple<CollidableType, CollidableType>(CollidableType.Player, CollidableType.Pickup_MultiBullet),
                    typeof(AddMultiBulletOnCollision)
                },
                {
                    new Tuple<CollidableType, CollidableType>(CollidableType.Player, CollidableType.Bullet),
                    typeof(QueryForRespawnOnCollision)
                },
                {
                    new Tuple<CollidableType, CollidableType>(CollidableType.Player, CollidableType.Meteor),
                    typeof(QueryForRespawnOnCollision)
                },
                {
                    new Tuple<CollidableType, CollidableType>(CollidableType.Player, CollidableType.Default),
                    typeof(QueryForRespawnOnCollision)
                },
            };
        public static ICollisionHandler GetCollisionBehaviour(CollidableType mainType, CollidableType secondaryType)
        {
            var tuple = new Tuple<CollidableType, CollidableType>(mainType, secondaryType);

            var collisionBehaviour = SRegisteredTypes.ContainsKey(tuple)
                ? SRegisteredTypes[tuple]
                : typeof(DestroyOnCollision);
            
            return Activator.CreateInstance(collisionBehaviour) as ICollisionHandler;
        }
    }
}