using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public class PlayerShootingSystem : SystemBase
    {
        private float _elapsedTime;
        private CustomInput _input;
        protected override void OnCreate()
        {
            //TODO: is there a better method to handle the new input system?
            //probably wrap it in a component and pass it as singleton
            _input = new CustomInput();
            _input.Keyboard.Enable();
            RequireSingletonForUpdate<Player>();
        }

        protected override void OnUpdate()
        {
            var shooterEntity = GetSingletonEntity<Player>();
            var shooterData = EntityManager.GetComponentData<Player>(shooterEntity);
            
            _elapsedTime += Time.DeltaTime;
            if (!_input.Keyboard.ShipSkills.triggered || _elapsedTime < shooterData.FireCooldown) return;
            
            _elapsedTime = 0;
            var bulletAmount = EntityManager.HasComponent<MultiBulletPowerUp>(shooterEntity) ? 8 : 1;
            for (var i = 0; i < bulletAmount; ++i)
            {
                var bullet = EntityManager.Instantiate(shooterData.BulletPrefab);
                EntityManager.SetComponentData(bullet, new Rotation
                {
                    Value = math.mul(quaternion.RotateZ(2 * i * math.PI / 8),
                        EntityManager.GetComponentData<Rotation>(shooterEntity).Value)
                });
                EntityManager.SetComponentData(bullet, EntityManager.GetComponentData<Translation>(shooterEntity));
            }
            
        }
    }
}