using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public class PlayerShootingSystem : EntitySpawnerSystem<Player>
    {
        private CustomInput _input;
        protected override void OnCreate()
        {
            base.OnCreate();
            //TODO: is there a better method to handle the new input system?
            //probably wrap it in a component and pass it as singleton
            _input = new CustomInput();
            _input.Keyboard.Enable();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            _elapsedTime += Time.DeltaTime;
            if (!_input.Keyboard.ShipSkills.triggered || _elapsedTime < _spawnerData.FireCooldown) return;
            
            _elapsedTime = 0;
            SpawnEntity(_spawnerData.BulletPrefab);
        }

        protected override void SpawnEntity(Entity e)
        {
            var shooterEntity = GetSingletonEntity<Player>();
            var bulletAmount = EntityManager.HasComponent<MultiBulletPowerUp>(shooterEntity) ? 8 : 1;
            var ecb = _endSimulationEcbSystem.CreateCommandBuffer();
            for (var i = 0; i < bulletAmount; ++i)
            {
                var bullet = ecb.Instantiate(e);
                ecb.SetComponent(bullet, new Rotation
                {
                    Value = math.mul(quaternion.RotateZ(2 * i * math.PI / 8),
                        EntityManager.GetComponentData<Rotation>(shooterEntity).Value)
                });
                ecb.SetComponent(bullet, EntityManager.GetComponentData<Translation>(shooterEntity));
            }
        }
    }
}