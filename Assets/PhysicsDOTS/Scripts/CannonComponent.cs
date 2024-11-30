using Unity.Entities;

public struct CannonComponent : IComponentData
{
    public float ShootCooldown;
    public Entity BallPrefab;
}
