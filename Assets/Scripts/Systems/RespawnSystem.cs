using Components;
using GUI;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    public class RespawnSystem : SystemBase
    {
        private EntityQuery _collidablesQuery;
        private PlayerData _data;
        private CustomInput _input;
        
        protected override void OnCreate()
        {
            base.OnCreate();
            _collidablesQuery = GetEntityQuery(ComponentType.ReadOnly<Collidable>());
            RequireSingletonForUpdate<RequestRespawn>();
            RequireSingletonForUpdate<PlayerData>();
            _input = new CustomInput();
            _input.Keyboard.Enable();
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            _data = GetSingleton<PlayerData>();
        }

        protected override void OnUpdate()
        {
            if (!_collidablesQuery.IsEmpty)
            {
                GUIManager.Instance.ToggleGUI(true);
                EntityManager.AddComponent<DestroyTag>(_collidablesQuery);
            }
            
            if (!_input.Keyboard.ShipSkills.triggered) return;
            
            GUIManager.Instance.ToggleGUI(false);
            
            World.GetOrCreateSystem<MeteorSpawnerSystem>().Reset();
            World.GetOrCreateSystem<UfoSpawnerSystem>().Reset();
            EntityManager.DestroyEntity(GetSingletonEntity<RequestRespawn>());
            
            var player = EntityManager.Instantiate(_data.PlayerPrefab);
            EntityManager.SetComponentData(player, new Translation{ Value = _data.PlayerStartingPos });
        }
    }
}