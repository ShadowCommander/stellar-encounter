using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECS
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public class BulletSpawnerSystem : JobComponentSystem
    {
        BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

        protected override void OnCreate()
        {
            m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }

        [BurstCompile]
        public struct BulletSpawnerJob : IJobForEachWithEntity<Translation, Rotation, BulletSpawner>
        {
            public EntityCommandBuffer CommandBuffer;
            public float deltaTime;

            public void Execute(Entity entity, int index, [ReadOnly] ref Translation translation,
                [ReadOnly] ref Rotation rotation, [ReadOnly] ref BulletSpawner spawner)
            {
                spawner.Timer += deltaTime;

                if (spawner.Timer > spawner.FireRate)
                {
                    spawner.Timer -= spawner.FireRate;

                    var instance = CommandBuffer.Instantiate(spawner.Bullet);

                    float3 pos = translation.Value;
                    pos += math.mul(rotation.Value, spawner.Offset);

                    CommandBuffer.SetComponent(instance, new Translation { Value = pos });
                    CommandBuffer.SetComponent(instance, new Rotation { Value = rotation.Value });
                }
            }
        }

        // OnUpdate runs on the main thread.
        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var job = new BulletSpawnerJob()
            {
                deltaTime = Time.deltaTime,
                CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
            }.ScheduleSingle(this, inputDependencies);

            m_EntityCommandBufferSystem.AddJobHandleForProducer(job);
            return job;
        }
    }
}
