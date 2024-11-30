using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct TriggerSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        EntityManager entityManager = state.EntityManager;
        NativeArray<Entity> entities = entityManager.GetAllEntities(Allocator.Temp);
        EntityCommandBuffer cmd = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        foreach (Entity entity in entities)
        {
            if (entityManager.HasComponent<TriggerComponent>(entity))
            {
                RefRW<LocalToWorld> triggerTransform = SystemAPI.GetComponentRW<LocalToWorld>(entity);
                RefRO<TriggerComponent> triggerComponent = SystemAPI.GetComponentRO<TriggerComponent>(entity);

                float size = triggerComponent.ValueRO.size;
                triggerTransform.ValueRW.Value.c0 = new float4(size, 0, 0, 0);
                triggerTransform.ValueRW.Value.c1 = new float4(0, size, 0, 0);
                triggerTransform.ValueRW.Value.c2 = new float4(0, 0, size, 0);

                PhysicsWorldSingleton physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
                NativeList<ColliderCastHit> hits = new NativeList<ColliderCastHit>(Allocator.Temp);

                physicsWorld.SphereCastAll(triggerTransform.ValueRO.Position, triggerComponent.ValueRO.size / 2, float3.zero, 1, ref hits,
                    CollisionFilter.Default);

                foreach (ColliderCastHit hit in hits)
                {
                    cmd.DestroyEntity(hit.Entity);
                }

                cmd.Playback(entityManager);
            }
        }
    }
}
