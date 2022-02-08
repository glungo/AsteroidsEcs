using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public class PickupSpawnerSystem : EntitySpawnerSystem<PickupSpawnerData>
    {
        private CameraBounds _cameraBounds;

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            _cameraBounds = GetSingleton<CameraBounds>();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            if (_spawnedAmount >= _spawnerData.PickupsPerRound || _elapsedTime < _spawnerData.Cooldown) return;

            SpawnEntity(_random.NextBool() ? _spawnerData._shieldPrefab : _spawnerData._multiBulletPrefab);
            _elapsedTime = 0;
        }
        protected override void SpawnEntity(Entity e)
        {
            _spawnedAmount++;
            var buffer = _endSimulationEcbSystem.CreateCommandBuffer();
            var entity = buffer.Instantiate(e);
            var bounds = _cameraBounds.CameraWorldSpaceBounds;
            buffer.SetComponent(entity, new Translation
            {
                Value = new float3(_random.NextFloat(bounds.x + bounds.y / 6, 5 * bounds.y / 6),
                    _random.NextFloat(bounds.z + bounds.w / 6, 5 * bounds.w / 6), 0)
            });
        }
    }
}