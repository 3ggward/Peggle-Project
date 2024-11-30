using Unity.Entities;
using Unity.Mathematics;

public struct BallComponent : IComponentData
{
    public float3 Velocity;
    public bool IsLaunched;
}
