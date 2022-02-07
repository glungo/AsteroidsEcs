using Components;
using Components.Utils;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

namespace Systems
{
    public class MeteorSpawnerSystem : SystemBase
    {
        /* TODO: there is a abstraction to be made here. All spawners will use a CommandBuffer
        control time and have a reference to the component holding the data for spawning */
        
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
            _random = new Random(123712);
            _random.InitState();
            _endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnStartRunning()
        {
            _spawnerEntity = GetSingletonEntity<MeteorSpawner>();
            _spawnerData = EntityManager.GetComponentData<MeteorSpawner>(_spawnerEntity);
            _cameraBounds = GetSingleton<CameraBounds>();
        }

        protected override void OnUpdate()
        {
            CreateSmallMeteors();

            _elapsedTime += Time.DeltaTime;
            if (_spawnedMeteors >= _spawnerData.MaxMeteors || _elapsedTime < _spawnerData.MeteorCooldown) return;

            SpawnRandomMeteor();

            ++_spawnedMeteors;
            _elapsedTime = 0;
        }

        private void CreateSmallMeteors()
        {
            var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
            Entities.ForEach((Entity e, int entityInQueryIndex, Translation tr, Meteor meteorComponent,
                SpawnSmallMeteorsTag tag) =>
            {
                for (var i = 0; i < meteorComponent.SmallMeteorAmount; ++i)
                {
                    var smallMeteor = ecb.Instantiate(entityInQueryIndex, meteorComponent.SmallMeteorPrefab);
                    ecb.SetComponent(entityInQueryIndex, smallMeteor, new Rotation
                    {
                        Value = quaternion.RotateZ(i * 2 * math.PI / meteorComponent.SmallMeteorAmount)
                    });
                    ecb.SetComponent(entityInQueryIndex, smallMeteor, tr);
                }

                ecb.AddComponent<DestroyTag>(entityInQueryIndex, e);
            }).ScheduleParallel();

            _endSimulationEcbSystem.AddJobHandleForProducer(Dependency);
        }

        private void SpawnRandomMeteor()
        {
            var buffer = _endSimulationEcbSystem.CreateCommandBuffer();
            var meteorEntities = EntityManager.GetBuffer<EntityBuffer>(_spawnerEntity);
            var meteor = buffer.Instantiate(meteorEntities[_random.NextInt(0, meteorEntities.Length)].Element);
            
            var side = _random.NextBool();
            buffer.SetComponent(meteor, new Rotation
            {
                Value = quaternion.RotateZ(side ? 1 : math.PI * _random.NextFloat(-math.PI / 8, math.PI / 8))
            });

            var bounds = _cameraBounds.CameraWorldSpaceBounds;
            buffer.SetComponent(meteor, new Translation
            {
                Value = new float3(_random.NextFloat(bounds.x, bounds.y), side ? bounds.z : bounds.w, 0)
            });
        }
    }
}