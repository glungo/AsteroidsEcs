using Unity.Entities;
using Unity.Mathematics;

namespace Systems
{
    public abstract class EntitySpawnerSystem<TData> : SystemBase where TData: struct, IComponentData
    {
        protected float _elapsedTime;
        protected int _spawnedAmount;
        protected Random _random;
        protected TData _spawnerData;
        
        protected EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireSingletonForUpdate<TData>();
            _random = new Random(123712);
            _random.InitState();
            _endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnStartRunning()
        {
           _spawnerData = GetSingleton<TData>();
        }

        protected override void OnUpdate()
        {
            _elapsedTime += Time.DeltaTime;
        }

        protected abstract void SpawnEntity(Entity e);

        public void Reset()
        {
            _elapsedTime = 0;
            _spawnedAmount = 0;
        }
    }
}