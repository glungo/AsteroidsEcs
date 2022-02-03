using Entities;
using Physics;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    public class BulletSpawnerSystem : SystemBase
    {
        private float _elapsedTime;
        private CustomInput _input;

        protected override void OnCreate()
        {
            //TODO: is there a better method to handle the new input system?
            _input = new CustomInput();
            _input.Keyboard.Enable();
            RequireSingletonForUpdate<Shooter>();
        }

        protected override void OnUpdate()
        {
            var shooterEntity = GetSingletonEntity<Shooter>();
            var shooterData = EntityManager.GetComponentData<Shooter>(shooterEntity);
            
            _elapsedTime += Time.DeltaTime;
            if (!_input.Keyboard.ShipSkills.triggered || _elapsedTime < shooterData.FireCooldown) return;
            
            _elapsedTime = 0;
            var bullet = EntityManager.Instantiate(shooterData.BulletPrefab);
            EntityManager.SetComponentData(bullet, EntityManager.GetComponentData<Rotation>(shooterEntity));
            EntityManager.SetComponentData(bullet, EntityManager.GetComponentData<Translation>(shooterEntity));
        }
    }
}