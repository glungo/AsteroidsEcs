using System.Collections.Generic;
using Components.Utils;
using Unity.Entities;
using UnityEngine;

namespace Components.Authoring
{
    public class MeteorSpawnerAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public List<GameObject> MeteorPrefabs;
        [SerializeField] private float MeteorSpawnCooldown = 0.05f;
        [SerializeField] private int MaxMeteorsPerRun = 20;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new MeteorSpawner()
            {
                MeteorCooldown = MeteorSpawnCooldown,
                MaxMeteors = MaxMeteorsPerRun
            });
            
            var buffer = dstManager.AddBuffer<EntityBuffer>(entity);

            if (MeteorPrefabs == null)
                return;
            
            foreach (var meteor in MeteorPrefabs)
            {
                buffer.Add(new EntityBuffer()
                {
                    Element = conversionSystem.GetPrimaryEntity(meteor)
                });
            }
        }
    }

    [UpdateInGroup(typeof(GameObjectDeclareReferencedObjectsGroup))]
    internal class DeclareMeteorSpawnerReference : GameObjectConversionSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((MeteorSpawnerAuthoring auth) =>
            {
                if (auth.MeteorPrefabs == null)
                    return;
                
                foreach (var s in auth.MeteorPrefabs)
                {
                    DeclareReferencedPrefab(s);
                }
            });
        }
    }
}