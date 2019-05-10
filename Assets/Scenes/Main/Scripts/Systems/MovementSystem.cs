using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECS
{
    public class MovementSystem : JobComponentSystem
    {
        [BurstCompile]
        public struct VelocityJob : IJobForEach<Translation, Rotation, Velocity>
        {
            public float deltaTime;

            public void Execute(ref Translation translation, [ReadOnly] ref Rotation rotation, [ReadOnly] ref Velocity velocity)
            {
                float3 value = translation.Value;
                value += deltaTime * velocity.Value * math.mul(rotation.Value, new float3(1f, 0f, 0f));
                translation.Value = value;
            }
        }

        // OnUpdate runs on the main thread.
        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var job = new VelocityJob()
            {
                deltaTime = Time.deltaTime
            };

            return job.Schedule(this, inputDependencies);
        }
    }
}
