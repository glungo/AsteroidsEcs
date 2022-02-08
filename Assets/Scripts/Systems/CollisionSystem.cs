using Components;
using Systems.Collision_Handling;
using Unity.Entities;
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
            if (!CollidableGroup.HasComponent(collisionEvent.EntityA) ||
                !CollidableGroup.HasComponent(collisionEvent.EntityB)) return;
            var typeA = CollidableGroup[collisionEvent.EntityA].Type;
            var typeB = CollidableGroup[collisionEvent.EntityB].Type;
            
            CollisionBehaviourFactory.GetCollisionBehaviour(typeB, typeA).OnCollision(Ecb, collisionEvent.EntityB);
            if (CollidableGroup[collisionEvent.EntityA].Invulnerable) return;
            CollisionBehaviourFactory.GetCollisionBehaviour(typeA, typeB).OnCollision(Ecb, collisionEvent.EntityA);
        }
    }
    
    public class CollisionSystem : SystemBase
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

        protected override void OnUpdate()
        {
            Dependency = new CollisionJob
            {
                Ecb = _commandBufferSystem.CreateCommandBuffer(),
                CollidableGroup = GetComponentDataFromEntity<Collidable>()
            }.Schedule(_stepPhysicsWorldSystem.Simulation, ref _buildPhysicsWorldSystem.PhysicsWorld, Dependency);
            
            _commandBufferSystem.AddJobHandleForProducer(Dependency);
            CompleteDependency();
        }
    }
}