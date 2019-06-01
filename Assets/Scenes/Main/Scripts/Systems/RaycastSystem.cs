using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

using RaycastHit = Unity.Physics.RaycastHit;

namespace ECS
{
    public class RaycastSystem : JobComponentSystem
    {
        private static Unity.Physics.Systems.BuildPhysicsWorld physicsWorldSystem;
        private static CollisionWorld collisionWorld;

        public void Start()
        {
            physicsWorldSystem = World.Active.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
            collisionWorld = physicsWorldSystem.PhysicsWorld.CollisionWorld;
        }

        [BurstCompile]
        public struct PrepareRaycastJob : IJobForEach<Velocity, LocalToWorld, RaycastData>
        {
            [ReadOnly] public CollisionWorld world;

            public void Execute([ReadOnly]  ref Velocity velocity, [ReadOnly] ref LocalToWorld localToWorld, ref RaycastData raycastData)
            {
                RaycastInput input = new RaycastInput()
                {
                    Start = localToWorld.Position,
                    End = localToWorld.Position + (localToWorld.Right * velocity.Value),
                    Filter = new CollisionFilter()
                    {
                        BelongsTo = ~0u,
                        CollidesWith = ~0u,
                        GroupIndex = 0
                    }
                };
                raycastData.Hit = world.CastRay(input, out raycastData.RaycastHit);
            }
        }

        // OnUpdate runs on the main thread.
        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var job = new PrepareRaycastJob()
            {
                world = collisionWorld
            };

            return job.ScheduleSingle(this, inputDependencies);
        }
    }
}
