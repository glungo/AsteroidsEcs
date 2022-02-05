using Entities;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public class WarpSystem : SystemBase
    {
        private const float TOLERANCE = .4f;
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

            Entities.ForEach((Entity e, int entityInQueryIndex, ref Translation tr, ref LocalToWorld ltw,
                ref BoundsBehaviour bb) =>
            {
                var pos = ltw.Value.c3.xy;
                var boundsX = pos.x < bounds.x - TOLERANCE;
                var boundsY = pos.x > bounds.y + TOLERANCE;
                var boundsZ = pos.y < bounds.z - TOLERANCE;
                var boundsW = pos.y > bounds.w + TOLERANCE;
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
