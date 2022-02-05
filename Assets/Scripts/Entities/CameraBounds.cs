using Unity.Entities;
using Unity.Mathematics;

namespace Entities
{
    [GenerateAuthoringComponent]
    public struct CameraBounds : IComponentData
    {
        public float4 CameraWorldSpaceBounds;
    }
}