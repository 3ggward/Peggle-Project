using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using Unity.Physics;

public partial struct CannonSystem : ISystem
{
    private Entity cannonEntity;
    private Entity inputEntity;
    private EntityManager entityManager;
    private CannonComponent cannonComponent;
    private InputComponent inputComponent;
    public void OnUpdate(ref SystemState state)
    {
        entityManager = state.EntityManager;
        cannonEntity = SystemAPI.GetSingletonEntity<CannonComponent>();
        inputEntity = SystemAPI.GetSingletonEntity<InputComponent>();

        cannonComponent = entityManager.GetComponentData<CannonComponent>(cannonEntity);
        inputComponent = entityManager.GetComponentData<InputComponent>(inputEntity);

        Shoot(ref state);
    }

    private float nextShootTime;
    private void Shoot(ref SystemState state)
    {
        LocalTransform cannonTransform = entityManager.GetComponentData<LocalTransform>(cannonEntity);

        Vector2 dir = (Vector2)inputComponent.MousePos - (Vector2)Camera.main.WorldToScreenPoint(cannonTransform.Position);
        float angle = math.degrees(math.atan2(dir.y, dir.x));
        cannonTransform.Rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        entityManager.SetComponentData(cannonEntity, cannonTransform);

        if (inputComponent.PressingLMB && nextShootTime < SystemAPI.Time.ElapsedTime )
        {
            EntityCommandBuffer cmd = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

            Entity ballEntity = entityManager.Instantiate(cannonComponent.BallPrefab);

            LocalTransform ballTransform = entityManager.GetComponentData<LocalTransform>(ballEntity);
            PhysicsVelocity ballVelocity = entityManager.GetComponentData<PhysicsVelocity>(ballEntity);
            ballTransform.Rotation = cannonTransform.Rotation;
            ballTransform.Position = new float3(0, (float) 5.5, (float) -3.5);

            if (inputComponent.MousePos.x < 960)
            {
                ballVelocity.Linear = new float3((960 - inputComponent.MousePos.x) / -98, 0, 0);
            } else
            {
                ballVelocity.Linear = new float3((inputComponent.MousePos.x - 960) / 98, 0, 0);
            }
            
            cmd.SetComponent(ballEntity, ballTransform);
            cmd.SetComponent(ballEntity, ballVelocity);

            cmd.Playback(entityManager);

            nextShootTime = (float)SystemAPI.Time.ElapsedTime + cannonComponent.ShootCooldown;
        }
    }
}
