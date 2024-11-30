using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CannonAuthoring : MonoBehaviour
{
    public float ShootCooldown = 1;
    public GameObject BallPrefab;
    public GameObject TriggerPrefab;

    public class CannonBaker : Baker<CannonAuthoring>
    {
        public override void Bake(CannonAuthoring authoring)
        {
            Entity cannonEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(cannonEntity, new CannonComponent 
            { 
                ShootCooldown = authoring.ShootCooldown,
                BallPrefab = GetEntity(authoring.BallPrefab, TransformUsageFlags.None)
            });
        }
    }
}
