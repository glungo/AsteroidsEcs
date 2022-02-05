using Entities;
using Entities.Utils;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

namespace Systems
{
    public class MeteorSpawnerSystem : SystemBase
    {
        private float _elapsedTime;
        private int _spawnedMeteors;
        private Random _random;
        private Entity _spawnerEntity;
        private MeteorSpawner _spawnerData;
        private CameraBounds _cameraBounds;
        
        private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireSingletonForUpdate<MeteorSpawner>();
            _endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            _random = new Random(123712);
        }

        protected override void OnStartRunning()
        {
            _spawnerEntity = GetSingletonEntity<MeteorSpawner>();
            _spawnerData = EntityManager.GetComponentData<MeteorSpawner>(_spawnerEntity);
            _cameraBounds = GetSingleton<CameraBounds>();
        }

        protected override void OnUpdate()
        {
            _elapsedTime += Time.DeltaTime;
            if (_spawnedMeteors >= _spawnerData.MaxMeteors || _elapsedTime < _spawnerData.MeteorCooldown) return;

            SpawnRandomMeteor();

            ++_spawnedMeteors;
            _elapsedTime = 0;
        }

        private void SpawnRandomMeteor()
        {
            var buffer = _endSimulationEcbSystem.CreateCommandBuffer();

            var meteorEntities = EntityManager.GetBuffer<EntityBuffer>(_spawnerEntity);
            var prefabCount = meteorEntities.Length;
            var meteor = buffer.Instantiate(meteorEntities[_random.NextInt(0, prefabCount)].Element);
            var bounds = _cameraBounds.CameraWorldSpaceBounds;
            var side = _random.NextBool();
            var pos = new float3(_random.NextFloat(bounds.x, bounds.y), side ? bounds.z : bounds.w, 0);

            buffer.SetComponent(meteor, new Rotation
            {
                Value = quaternion.RotateZ(side ? 1 : math.PI * _random.NextFloat(-math.PI / 8, math.PI / 8))
            });

            buffer.SetComponent(meteor, new Translation
            {
                Value = pos
            });
        }
    }
}