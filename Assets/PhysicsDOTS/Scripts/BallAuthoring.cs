using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BallAuthoring : MonoBehaviour
{
    public float3 Velocity;
    public bool IsLaunched;

    public class BallBaker : Baker<BallAuthoring>
    {
        public override void Bake(BallAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new BallComponent { Velocity = authoring.Velocity, IsLaunched = authoring.IsLaunched });
        }
    }
}
