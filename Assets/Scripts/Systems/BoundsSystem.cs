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
            //TODO: is there a better way to pass reference types to lamdas? (like c++ [ref](params){})
            var camera = Camera.main;
            var camSize = camera.orthographicSize;
            var camAspect = camera.aspect;
            var bounds = new float4(0, 2 * camAspect * camSize, 0, 2 * camSize);
            
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
                    if (boundsX)
                        pos.x = bounds.y - math.EPSILON;
                    if (boundsY)
                        pos.x = bounds.x + math.EPSILON;
                    if (boundsZ)
                        pos.y = bounds.w - math.EPSILON;
                    if (boundsW)
                        pos.y = bounds.z + math.EPSILON;
                    
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
