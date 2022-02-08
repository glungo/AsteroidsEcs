using Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Systems
{
    public class InputMovementSystem : SystemBase
    {
        private CustomInput _input;
        private Random _random;
        protected override void OnCreate()
        {
            _input = new CustomInput();
            _input.Keyboard.Enable();
            _random = new Random(1239107);
            _random.InitState();
            RequireSingletonForUpdate<InputDrivenMovement>();
        }

        protected override void OnUpdate()
        {
            var xAxis = _input.Keyboard.Movement.ReadValue<Vector2>().x;
            var yAxis = _input.Keyboard.Movement.ReadValue<Vector2>().y;
            var deltaTime = Time.DeltaTime;

            Entities.WithAll<InputDrivenMovement, PhysicsDrivenMovement>().ForEach(
                (ref InputDrivenMovement id, ref PhysicsDrivenMovement pd) =>
                {
                    pd.Speed += yAxis * id.SpeedMultiplier * deltaTime;
                    pd.AngularSpeed += xAxis * (-1) * id.AngularSpeedMultiplier * deltaTime;
                }).Run();
            
            var warp = _input.Keyboard.ShipWarp.triggered;
            if (!warp) return;
            var player = GetSingletonEntity<Player>();
            var camBounds = GetSingleton<CameraBounds>().CameraWorldSpaceBounds;
            EntityManager.SetComponentData(player, new Translation
            {
                Value = new float3(_random.NextFloat(camBounds.y), _random.NextFloat(camBounds.w), 0)
            });
        }
    }
}
