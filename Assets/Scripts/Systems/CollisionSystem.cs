using Entities;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;

namespace Systems
{ 
    internal struct CollisionJob : ICollisionEventsJob
    {
        public EntityCommandBuffer Ecb;
        public ComponentDataFromEntity<Collidable> CollidableGroup;

        public void Execute(CollisionEvent collisionEvent)
        {
            for (var i = 0; i < 2; ++i)
            {
                var entity = i == 0? collisionEvent.EntityA : collisionEvent.EntityB;
                if (!CollidableGroup.HasComponent(entity)) continue;
                
                var type = CollidableGroup[entity].Type;
                CollisionBehaviourFactory.GetCollisionBehaviour(type).OnCollision(Ecb, entity);
            }
        }
    }
    
    public class CollisionSystem : JobComponentSystem
    {
        private BuildPhysicsWorld _buildPhysicsWorldSystem;
        private StepPhysicsWorld _stepPhysicsWorldSystem;
        private EndSimulationEntityCommandBufferSystem _commandBufferSystem;

        protected override void OnCreate() {
            base.OnCreate();
            _buildPhysicsWorldSystem = World.GetOrCreateSystem<BuildPhysicsWorld>();
            _stepPhysicsWorldSystem = World.GetOrCreateSystem<StepPhysicsWorld>();
            _commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var jobHandle = new CollisionJob
            {
                Ecb = _commandBufferSystem.CreateCommandBuffer(),
                CollidableGroup = GetComponentDataFromEntity<Collidable>()
            }.Schedule(_stepPhysicsWorldSystem.Simulation, ref _buildPhysicsWorldSystem.PhysicsWorld, inputDeps);
            
            _commandBufferSystem.AddJobHandleForProducer(jobHandle);
            return jobHandle;    
        }
    }
}