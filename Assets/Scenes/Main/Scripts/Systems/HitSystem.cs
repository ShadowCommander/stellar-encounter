using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECS
{
    [UpdateAfter(typeof(RaycastSystem))]
    public class HitSystem : JobComponentSystem
    {
        BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

        protected override void OnCreate()
        {
            m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }

        [BurstCompile]
        public struct HitJob : IJobForEachWithEntity<RaycastData, Damage>
        {
            public EntityCommandBuffer CommandBuffer;

            public void Execute(Entity entity, int index, [ReadOnly] ref RaycastData raycastData, [ReadOnly] ref Damage damage)
            {
                if (raycastData.Hit)
                {
                    Unity.Physics.Systems.BuildPhysicsWorld physicsWorldSystem = World.Active.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();

                    Entity e = physicsWorldSystem.PhysicsWorld.Bodies[raycastData.RaycastHit.RigidBodyIndex].Entity;

                    CommandBuffer.SetComponent(e, new Damaged { Value = damage.Value });
                }
            }
        }

        // OnUpdate runs on the main thread.
        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var job = new HitJob()
            {
                CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
            }.ScheduleSingle(this, inputDependencies);

            m_EntityCommandBufferSystem.AddJobHandleForProducer(job);
            return job;
        }
    }
}
