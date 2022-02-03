using Physics;
using Unity.Entities;
using UnityEngine;

namespace Input
{
    public class InputSystem : SystemBase
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
            Entities.WithAll<InputDriven, PhysicsDriven>().ForEach((ref InputDriven id, ref PhysicsDriven pd) =>
            {
                pd.Speed += yAxis * id.SpeedMultiplier * deltaTime;
                pd.AngularSpeed += xAxis * (-1) * id.AngularSpeedMultiplier * deltaTime;
            }).ScheduleParallel();
        }
    }
}