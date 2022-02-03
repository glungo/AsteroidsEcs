using Unity.Entities;
using UnityEngine;

namespace Physics
{
    public class PhysicsDrivenComponent : MonoBehaviour, IComponentData, IConvertGameObjectToEntity
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _angularSpeed;
        [SerializeField] private float _angularAccel;
        [SerializeField] private float _accel;

        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new PhysicsDriven
            {
                Speed = _speed,
                AngularSpeed = _angularSpeed,
                Accel = _accel,
                AngularAccel = _angularAccel
            });
        }
    }
}