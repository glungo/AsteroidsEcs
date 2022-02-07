using Components;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    public class InputMovementSystem : SystemBase
    {
        private CustomInput _input;
    
        protected override void OnCreate()
        {
            _input = new CustomInput();
            _input.Keyboard.Enable();
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
        }
    }
}
