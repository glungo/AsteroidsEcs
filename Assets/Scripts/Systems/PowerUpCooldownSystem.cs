using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public class PowerUpCooldownSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;
        protected override void OnCreate()
        {
            base.OnCreate();
            _endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }
        
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            var buffer = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
            
            Entities.ForEach((Entity entity, int entityInQueryIndex,  ref MultiBulletPowerUp powerUp) =>
            {
                powerUp.RunningTime += deltaTime;
                if (powerUp.RunningTime < powerUp.Duration) return;
                buffer.RemoveComponent<MultiBulletPowerUp>(entityInQueryIndex, entity);
            }).ScheduleParallel();

            var shieldContainer = GetSingletonEntity<ShieldContainer>();
            var shieldPos = GetComponent<Translation>(shieldContainer);
            Entities.ForEach((Entity entity, int entityInQueryIndex, ref ShieldPowerUp powerUp,
                ref Collidable collidable) =>
            {
                powerUp.RunningTime += deltaTime;
                buffer.SetComponent(entityInQueryIndex, shieldContainer, new Translation
                {
                    Value = new float3(shieldPos.Value.x, shieldPos.Value.y, 0)
                });
                collidable.Invulnerable = true;
                if (powerUp.RunningTime < powerUp.Duration) return;
                //HACK: DisableRendering does not work with SpriteRenderer. The correct solution would probably be to
                //switch into project tiny
                buffer.SetComponent(entityInQueryIndex, shieldContainer, new Translation
                {
                    Value = new float3(shieldPos.Value.x, shieldPos.Value.y, -9999)
                });
                collidable.Invulnerable = false;
                buffer.RemoveComponent<ShieldPowerUp>(entityInQueryIndex, entity);
            }).ScheduleParallel();
            
            _endSimulationEcbSystem.AddJobHandleForProducer(Dependency);
        }
    }
}