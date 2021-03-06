﻿using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECS
{
    [UpdateAfter(typeof(HitSystem))]
    public class DamageSystem : JobComponentSystem
    {
        BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

        protected override void OnCreate()
        {
            m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }

        [BurstCompile]
        public struct DamageJob : IJobForEachWithEntity<Damaged, Health>
        {
            public EntityCommandBuffer CommandBuffer;

            public void Execute(Entity entity, int index, [ReadOnly] ref Damaged damaged, ref Health health)
            {
                health.Value -= damaged.Value;
                if (health.Value < 0)
                {
                    CommandBuffer.DestroyEntity(entity);
                }
                else
                {
                    CommandBuffer.RemoveComponent<Damaged>(entity);
                }
            }
        }

        // OnUpdate runs on the main thread.
        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var job = new DamageJob()
            {
                CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
            }.ScheduleSingle(this, inputDependencies);

            m_EntityCommandBufferSystem.AddJobHandleForProducer(job);
            return job;
        }
    }
}
