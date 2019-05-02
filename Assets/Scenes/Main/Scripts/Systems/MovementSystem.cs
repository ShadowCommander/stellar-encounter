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
        public struct MoveSpeedJob : IJobForEach<Translation, Rotation, Movement>
        {
            public float deltaTime;

            public void Execute(ref Translation translation, [ReadOnly] ref Rotation rotation, [ReadOnly] ref Movement speed)
            {
                float3 value = translation.Value;
                value += deltaTime * speed.Value * math.mul(rotation.Value, new float3(1f, 0f, 0f));
                translation.Value = value;
            }
        }

        // OnUpdate runs on the main thread.
        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var job = new MoveSpeedJob()
            {
                deltaTime = Time.deltaTime
            };

            return job.Schedule(this, inputDependencies);
        }
    }
}
