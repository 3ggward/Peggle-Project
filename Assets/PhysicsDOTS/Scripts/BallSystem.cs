using Unity.Burst;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Collections;

partial struct BallSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {

    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach((
            RefRW<PhysicsVelocity> physicsVelocity,
            RefRW<BallComponent> ball,
            RefRW<PhysicsMass> physicsMass,
            RefRO<LocalTransform> localTransform)
            in SystemAPI.Query<
                RefRW<PhysicsVelocity>,
                RefRW<BallComponent>,
                RefRW<PhysicsMass>,
                RefRO<LocalTransform>>()) {
            physicsMass.ValueRW.InverseInertia.z = (float) 0;
            physicsVelocity.ValueRW.Linear.z = 0;
        } 
    }

    [BurstCompile]
    public void OnDelete(ref SystemState state)
    {

    }
}
