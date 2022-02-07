using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

namespace Systems
{
    public class UfoSpawnerSystem : SystemBase
    {
        private float _elapsedTime;
        private int _spawnedCount;
        private Random _random;
        private UfoSpawner _spawnerData;
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
            _spawnerData = GetSingleton<UfoSpawner>();
            _cameraBounds = GetSingleton<CameraBounds>();
        }

        protected override void OnUpdate()
        {
            _elapsedTime += Time.DeltaTime;
            if (_spawnedCount >= _spawnerData.UfoPerRound || _elapsedTime < _spawnerData.UfoSpawnCooldown) return;

            SpawnEntity();

            ++_spawnedCount;
            _elapsedTime = 0;
        }
        
        private void SpawnEntity()
        {
            var buffer = _endSimulationEcbSystem.CreateCommandBuffer();
            var entity = buffer.Instantiate(_spawnerData.UfoPrefab);
            
            var side = _random.NextBool();
            buffer.SetComponent(entity, new Rotation
            {
                Value = quaternion.RotateZ((side ? 1.5f * math.PI : math.PI/2) + _random.NextFloat(math.PI/4))
            });
            var bounds = _cameraBounds.CameraWorldSpaceBounds;
            buffer.SetComponent(entity, new Translation
            {
                Value = new float3(side ? bounds.x : bounds.y,
                    _random.NextFloat(bounds.z + bounds.w / 6, 5 * bounds.w / 6), 0)
            });
        }
    }
}