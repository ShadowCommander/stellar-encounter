//using Unity.Burst;
//using Unity.Collections;
//using Unity.Entities;
//using Unity.Jobs;
//using Unity.Mathematics;
//using Unity.Transforms;
//using UnityEngine;

//namespace ECS
//{
//    public class RaycastSystem : JobComponentSystem
//    {
//        NativeArray<RaycastHit> RaycastHits;

//        [BurstCompile]
//        public struct RaycastJob : IJobForEach<Translation, Rotation, RaycastData>
//        {
//            public float deltaTime;

//            public void Execute(ref Translation translation, [ReadOnly] ref Rotation rotation, [ReadOnly] ref RaycastData raycastData)
//            {

//            }
//        }

//        // OnUpdate runs on the main thread.
//        protected override JobHandle OnUpdate(JobHandle inputDependencies)
//        {
//            var job = new RaycastJob()
//            {
//                deltaTime = Time.deltaTime
//            };

//            return job.Schedule(this, inputDependencies);
//        }
//    }
//}
