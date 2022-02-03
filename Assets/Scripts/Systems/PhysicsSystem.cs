using Physics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    public class PhysicsSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            Entities.WithAll<PhysicsDrivenMovement>().ForEach(
                (ref Translation tr, ref Rotation rot, ref PhysicsDrivenMovement pd) =>
                {
                    rot.Value = math.mul(rot.Value, quaternion.RotateZ(pd.AngularSpeed * deltaTime));
                    tr.Value += math.mul(rot.Value, new float3(0, pd.Speed * deltaTime, 0));

                    pd.AngularSpeed -= math.sign(pd.AngularSpeed) * pd.AngularDrag * deltaTime;
                    pd.Speed -= math.sign(pd.Speed) * pd.Drag * deltaTime;
                }).ScheduleParallel();
        }
    }
}