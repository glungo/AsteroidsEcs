using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public class UfoAimingSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;
        protected override void OnCreate()
        {
            base.OnCreate();
            _endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            RequireSingletonForUpdate<Player>();
        }
        
        protected override void OnUpdate()
        {
            var playerPosition = EntityManager.GetComponentData<Translation>(GetSingletonEntity<Player>()).Value;
            var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
            var deltaTime = Time.DeltaTime;
            Entities.ForEach((Entity entity, int entityInQueryIndex, ref Rotation rot, ref Translation translation, ref Ufo ufo, in LocalToWorld ltw) =>
            {
                //shoot player
                ufo.ElapsedTimeSinceLastShot += deltaTime;
                if (ufo.ElapsedTimeSinceLastShot < ufo.FireCooldown) return;
                
                var dir = playerPosition - translation.Value;
                Debug.DrawRay(translation.Value, dir);
                var bullet = ecb.Instantiate(entityInQueryIndex, ufo.BulletPrefab);
                ecb.SetComponent(entityInQueryIndex, bullet, new Rotation
                {
                    Value = quaternion.LookRotation(ltw.Forward,math.normalize(dir))
                });
                ecb.SetComponent(entityInQueryIndex, bullet, translation);
                ufo.ElapsedTimeSinceLastShot = 0;

            }).ScheduleParallel();
        }
    }
}