using Entities;
using Unity.Entities;

namespace Systems
{
    public class EntityDestructionSystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _commandBufferSystem;

        protected override void OnCreate() {
            base.OnCreate();
            _commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var ecb = _commandBufferSystem.CreateCommandBuffer().AsParallelWriter();

            Entities.ForEach((Entity e, DestroyTag tag, int entityInQueryIndex) =>
            {
                ecb.DestroyEntity(entityInQueryIndex, e);
            }).ScheduleParallel();
            _commandBufferSystem.AddJobHandleForProducer(Dependency);
            CompleteDependency();
        }
    }
}