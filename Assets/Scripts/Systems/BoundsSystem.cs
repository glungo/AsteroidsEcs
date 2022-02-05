using Entities;
using Physics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Systems
{
    public class WarpSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;
        protected override void OnCreate()
        {
            base.OnCreate();
            _endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var bounds = GetSingleton<CameraBounds>().CameraWorldSpaceBounds;
            var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
            
            Entities.ForEach((Entity e, int entityInQueryIndex, ref Translation tr, ref LocalToWorld ltw, ref BoundsBehaviour bb) =>
            {
                var pos = ltw.Value.c3.xy;
                var boundsX = pos.x < bounds.x;
                var boundsY = pos.x > bounds.y;
                var boundsZ = pos.y < bounds.z;
                var boundsW = pos.y > bounds.w;
                if (!boundsX && !boundsY && !boundsZ && !boundsW) return;
                if (bb.WrapOnBound)
                {
                    if (boundsX || boundsY)
                        pos.x = bounds.y - pos.x;
                    if (boundsZ || boundsW)
                        pos.y = bounds.w - pos.y;

                    tr.Value = new float3(pos.x, pos.y, 0);
                }
                else
                {
                    ecb.DestroyEntity(entityInQueryIndex, e);
                }
            }).ScheduleParallel();
            
            _endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
        }
    }
}
