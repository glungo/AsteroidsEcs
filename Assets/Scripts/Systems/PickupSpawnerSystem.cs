using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public class PickupSpawnerSystem : SystemBase
    {
        private float _elapsedTime;
        private int _spawnedCount;
        private Random _random;
        private PickupSpawnerData _spawnerData;
        private CameraBounds _cameraBounds;
        
        private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireSingletonForUpdate<UfoSpawner>();
            _random = new Random(123712);
            _random.InitState();
            _endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnStartRunning()
        {
            _spawnerData = GetSingleton<PickupSpawnerData>();
            _cameraBounds = GetSingleton<CameraBounds>();
        }

        protected override void OnUpdate()
        {
            _elapsedTime += Time.DeltaTime;
            if (_spawnedCount >= _spawnerData.PickupsPerRound || _elapsedTime < _spawnerData.Cooldown) return;

            SpawnEntity();

            ++_spawnedCount;
            _elapsedTime = 0;
        }
        
        private void SpawnEntity()
        {
            var buffer = _endSimulationEcbSystem.CreateCommandBuffer();
            var entity =
                buffer.Instantiate(_random.NextBool() ? _spawnerData._shieldPrefab : _spawnerData._multiBulletPrefab);
            
            var bounds = _cameraBounds.CameraWorldSpaceBounds;
            buffer.SetComponent(entity, new Translation
            {
                Value = new float3(_random.NextFloat(bounds.x + bounds.y / 6, 5 * bounds.y / 6),
                    _random.NextFloat(bounds.z + bounds.w / 6, 5 * bounds.w / 6), 0)
            });
        }
        
        public void Reset()
        {
            _spawnedCount = 0;
            _elapsedTime = 0;
        }
    }
}