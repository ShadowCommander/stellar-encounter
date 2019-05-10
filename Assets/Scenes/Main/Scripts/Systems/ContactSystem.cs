using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Physics;
using Unity.Mathematics;
using UnityEngine;

using RaycastHit = Unity.Physics.RaycastHit;
using Ray = Unity.Physics.Ray;

namespace ECS
{
    public class ContactSystem : JobComponentSystem
    {
        private NativeList<RaycastCommand> raycastCommands;
        private NativeList<RaycastHit> raycastHits;

        [BurstCompile]
        public struct ContactJob : IJobForEachWithEntity<Translation, Rotation, Contact>
        {
            public float deltaTime;
            public NativeList<RaycastHit> raycastHits;

            public void Execute(Entity entity, int index, [ReadOnly] ref Translation translation, [ReadOnly] ref Rotation rotation, ref Contact contact)
            {
                // Use raycast to check for collision with other entities.
                // If found then set colliding to true and set entities list to colliding entities.
                // Length of raycast is collider + speed


            }
        }
        struct PrepareRaycastCommands : IJobForEachWithEntity<Translation, Rotation, Velocity>
        {
            public float DeltaTime;
            public NativeList<RaycastCommand> raycastCommands;

            //public void Execute(int i)
            //{
            //    // figure out how far the object we are testing collision for
            //    // wants to move in total. Our collision raycast only needs to be
            //    // that far.
            //    float distance = (Velocities[i] * DeltaTime).magnitude;
            //    Raycasts[i] = new RaycastCommand(Positions[i], Velocities[i], distance);
            //}

            public void Execute(Entity entity, int index, [ReadOnly] ref Translation translation, [ReadOnly] ref Rotation rotation, [ReadOnly] ref Velocity velocity)
            {
                //raycastInput = new RaycastInput
                //{
                //    Ray = new Ray { Origin = translation.Value, Direction = math.mul(rotation.Value, new float3(1f, 0f, 0f)) }
                //};
                raycastCommands.Add(new RaycastCommand(translation.Value, math.mul(rotation.Value, new float3(1f, 0f, 0f)), velocity.Value * DeltaTime));
            }
        }
        public void Start()
        {
            raycastCommands = new NativeList<RaycastCommand>(Allocator.Persistent);
        }

        // OnUpdate runs on the main thread.
        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var deltaTime = Time.deltaTime;

            var setupRaycastsJob = new PrepareRaycastCommands()
            {
                DeltaTime = deltaTime,
                raycastCommands = raycastCommands
            };

            var setupDependency = setupRaycastsJob.Schedule(this, inputDependencies);

            raycastHits = new NativeList<RaycastHit>(Allocator.Persistent);

            var raycastDependency = RaycastCommand.ScheduleBatch(raycastCommands, raycastHits, 32, setupDependency);

            var job = new ContactJob()
            {
                deltaTime = Time.deltaTime
            };

            return job.Schedule(this, inputDependencies);
        }
    }
}
