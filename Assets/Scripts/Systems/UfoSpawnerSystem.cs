using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public class UfoSpawnerSystem : EntitySpawnerSystem<UfoSpawner>
    {
        private CameraBounds _cameraBounds;
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireSingletonForUpdate<UfoSpawner>();
        }

        protected override void OnStartRunning()
        {
            _cameraBounds = GetSingleton<CameraBounds>();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            if (_spawnedAmount >= _spawnerData.UfoPerRound || _elapsedTime < _spawnerData.UfoSpawnCooldown) return;

            SpawnEntity(_spawnerData.UfoPrefab);
            _elapsedTime = 0;
            _spawnedAmount++;
        }

        protected override void SpawnEntity(Entity e)
        {
            var buffer = _endSimulationEcbSystem.CreateCommandBuffer();

            var side = _random.NextBool();
            buffer.SetComponent(e, new Rotation
            {
                Value = quaternion.RotateZ((side ? 1.5f * math.PI : math.PI/2) + _random.NextFloat(math.PI/4))
            });
            var bounds = _cameraBounds.CameraWorldSpaceBounds;
            buffer.SetComponent(e, new Translation
            {
                Value = new float3(side ? bounds.x : bounds.y,
                    _random.NextFloat(bounds.z + bounds.w / 6, 5 * bounds.w / 6), 0)
            });
        }
    }
}