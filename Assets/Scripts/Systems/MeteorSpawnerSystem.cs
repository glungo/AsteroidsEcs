using Components;
using Components.Utils;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public class MeteorSpawnerSystem : EntitySpawnerSystem<MeteorSpawner>
    {
        private CameraBounds _cameraBounds;
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireSingletonForUpdate<Player>();
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            _cameraBounds = GetSingleton<CameraBounds>();
        }

        protected override void OnUpdate()
        {
            CreateSmallMeteors();

            _elapsedTime += Time.DeltaTime;
            if (_spawnedAmount >= _spawnerData.MaxMeteors || _elapsedTime < _spawnerData.MeteorCooldown) return;
            var meteorPrefabs = EntityManager.GetBuffer<EntityBuffer>(GetSingletonEntity<MeteorSpawner>());
            SpawnEntity(meteorPrefabs[_random.NextInt(0, meteorPrefabs.Length)].Element);
            _elapsedTime = 0;
            _spawnedAmount++;
        }

        protected override void SpawnEntity(Entity e)
        {
            var buffer = _endSimulationEcbSystem.CreateCommandBuffer();
            var meteor = buffer.Instantiate(e);
            
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

        private void CreateSmallMeteors()
        {
            var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
            Entities.ForEach((Entity e, int entityInQueryIndex, in Translation tr, in Meteor meteorComponent,
                in SpawnSmallMeteorsTag tag) =>
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
    }
}