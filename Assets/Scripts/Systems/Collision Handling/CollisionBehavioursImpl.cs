using Components;
using Unity.Entities;

namespace Systems.Collision_Handling
{
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
}