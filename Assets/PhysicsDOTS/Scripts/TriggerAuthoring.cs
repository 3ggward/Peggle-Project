using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class TriggerAuthoring : MonoBehaviour
{
    public float size;

    public class TriggerBaker : Baker<TriggerAuthoring>
    {
        public override void Bake(TriggerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new TriggerComponent { size = authoring.size });
        }
    }
}
