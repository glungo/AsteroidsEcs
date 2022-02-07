using Unity.Entities;
using Unity.Mathematics;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct CameraBounds : IComponentData
    {
        public float4 CameraWorldSpaceBounds;
    }
}