using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECS
{
    public class ContactSystem : JobComponentSystem
    {
        [BurstCompile]
        public struct ContactJob : IJobForEach<Translation, Rotation, Contact>
        {
            public float deltaTime;

            public void Execute([ReadOnly] ref Translation translation, [ReadOnly] ref Rotation rotation, [ReadOnly] ref Contact speed)
            {
                // Use raycast to check for collision with other entities.
                // If found then set colliding to true and set entities list to colliding entities.
                // Length of raycast is collider + speed
            }
        }

        // OnUpdate runs on the main thread.
        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var job = new ContactJob()
            {
                deltaTime = Time.deltaTime
            };

            return job.Schedule(this, inputDependencies);
        }
    }
}
