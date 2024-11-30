using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PegAuthoring : MonoBehaviour
{
    public class PegBaker : Baker<TriggerAuthoring>
    {
        public override void Bake(TriggerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new PegComponent {  });
        }
    }
}