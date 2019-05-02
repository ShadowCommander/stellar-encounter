using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECS
{
    public class DamageSystem : JobComponentSystem
    {
        [BurstCompile]
        public struct DamageJob : IJobForEachWithEntity<Damage>
        {
            public float deltaTime;

            public void Execute(Entity entity, int index, [ReadOnly] ref Damage damage)
            {
                throw new System.NotImplementedException();
            }
        }

        // OnUpdate runs on the main thread.
        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var job = new DamageJob()
            {
                deltaTime = Time.deltaTime
            };

            return job.Schedule(this, inputDependencies);
        }
    }
}
