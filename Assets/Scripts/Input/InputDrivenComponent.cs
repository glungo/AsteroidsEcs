using Unity.Entities;
using UnityEngine;

namespace Input
{
    public class InputDrivenComponent : MonoBehaviour, IComponentData, IConvertGameObjectToEntity
    {
        [SerializeField] private float speedMultiplier;
        [SerializeField] private float angularSpeedMultiplier;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new InputDriven
            {
                SpeedMultiplier = speedMultiplier,
                AngularSpeedMultiplier = angularSpeedMultiplier
            });
        }
    }
}