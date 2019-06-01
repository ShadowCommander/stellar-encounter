//using Unity.Burst;
//using Unity.Collections;
//using Unity.Entities;
//using Unity.Jobs;
//using Unity.Transforms;
//using Unity.Physics;
//using Unity.Mathematics;
//using UnityEngine;

//using RaycastHit = Unity.Physics.RaycastHit;
//using Ray = Unity.Physics.Ray;

//namespace ECS
//{
//    public class ContactSystem : JobComponentSystem
//    {
//        private static Unity.Physics.Systems.BuildPhysicsWorld physicsWorldSystem;
//        private static CollisionWorld collisionWorld;
//        protected static NativeArray<RaycastInput> rayCommands;
//        protected static NativeArray<RaycastHit> rayResults;

//        public void Start()
//        {
//            physicsWorldSystem = World.Active.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
//            collisionWorld = physicsWorldSystem.PhysicsWorld.CollisionWorld;
//        }

//        [BurstCompile]
//        public struct ContactJob : IJobForEachWithEntity<Translation, Rotation, Contact>
//        {
//            public float deltaTime;

//            [BurstCompile]
//            public struct RaycastJob : IJobParallelFor
//            {
//                [ReadOnly] public CollisionWorld world;
//                [ReadOnly] public NativeArray<RaycastInput> inputs;
//                public NativeArray<RaycastHit> results;

//                public void Execute(int index)
//                {
//                    world.CastRay(inputs[index], out RaycastHit hit);
//                    results[index] = hit;
//                }
//            }

//            public static JobHandle ScheduleBatchRayCast(CollisionWorld world,
//                NativeArray<RaycastInput> inputs, NativeArray<RaycastHit> results)
//            {
//                JobHandle rcj = new RaycastJob
//                {
//                    inputs = inputs,
//                    results = results,
//                    world = world

//                }.Schedule(inputs.Length, 5);
//                return rcj;
//            }

//            public static void SingleRayCast(CollisionWorld world, RaycastInput input,
//    ref RaycastHit result)
//            {
//                rayCommands = new NativeArray<RaycastInput>(1, Allocator.Temp);
//                rayResults = new NativeArray<RaycastHit>(1, Allocator.Temp);
//                rayCommands[0] = input;
//                var handle = ScheduleBatchRayCast(world, rayCommands, rayResults);
//                handle.Complete();
//                result = rayResults[0];
//                rayCommands.Dispose();
//                rayResults.Dispose();
//            }

//            public void Execute(Entity entity, int index, [ReadOnly] ref Translation translation, [ReadOnly] ref Rotation rotation, ref Contact contact)
//            {
//                // Use raycast to check for collision with other entities.
//                // If found then set colliding to true and set entities list to colliding entities.
//                // Length of raycast is collider + speed
//                RaycastHit result = new RaycastHit();
//                SingleRayCast(collisionWorld, new RaycastInput() { Ray = new Ray() { Origin = contact.PrevPos, Direction = math.normalize(translation.Value - contact.PrevPos) } }, ref result);


//                //var results = new NativeArray<RaycastHit>(1, Allocator.Temp);
//                //var commands = new NativeArray<RaycastCommand>(1, Allocator.Temp);

//                //commands[0] = new RaycastCommand(translation.Value, math.normalize(translation.Value - contact.PrevPos), math.distance(translation.Value, contact.PrevPos));
//                ////contact.RaycastHits = RaycastAll(new Ray(contact.PrevPos, math.normalize(translation.Value - contact.PrevPos)), math.distance(translation.Value, contact.PrevPos))
//                //JobHandle handle = RaycastCommand.ScheduleBatch(commands, results, 1, default(JobHandle));

//                //handle.Complete();

//                //RaycastHit batchedHit = results[0];
//                //if (batchedHit.)


//                //results.Dispose();
//                //commands.Dispose();


//                contact.PrevPos = translation.Value;
//            }
//        }

//        // OnUpdate runs on the main thread.
//        protected override JobHandle OnUpdate(JobHandle inputDependencies)
//        {
//            var job = new ContactJob()
//            {
//                deltaTime = Time.deltaTime
//            };

//            return job.Schedule(this, inputDependencies);
//        }
//    }
//}

//private NativeList<RaycastCommand> raycastCommands;
//private NativeList<RaycastHit> raycastHits;

//struct PrepareRaycastCommands : IJobForEachWithEntity<Translation, Rotation, Velocity>
//{
//    public float DeltaTime;
//    public NativeList<RaycastCommand> raycastCommands;

//    //public void Execute(int i)
//    //{
//    //    // figure out how far the object we are testing collision for
//    //    // wants to move in total. Our collision raycast only needs to be
//    //    // that far.
//    //    float distance = (Velocities[i] * DeltaTime).magnitude;
//    //    Raycasts[i] = new RaycastCommand(Positions[i], Velocities[i], distance);
//    //}

//    public void Execute(Entity entity, int index, [ReadOnly] ref Translation translation, [ReadOnly] ref Rotation rotation, [ReadOnly] ref Velocity velocity)
//    {
//        //raycastInput = new RaycastInput
//        //{
//        //    Ray = new Ray { Origin = translation.Value, Direction = math.mul(rotation.Value, new float3(1f, 0f, 0f)) }
//        //};
//        //raycastCommands.Add(new RaycastCommand(translation.Value, math.mul(rotation.Value, new float3(1f, 0f, 0f)), velocity.Value * DeltaTime));
//    }
//}
//public void Start()
//{
//    raycastCommands = new NativeList<RaycastCommand>(Allocator.Persistent);
//}


//var setupRaycastsJob = new PrepareRaycastCommands()
//{
//    DeltaTime = deltaTime,
//    raycastCommands = raycastCommands
//};

//var setupDependency = setupRaycastsJob.Schedule(this, inputDependencies);

//raycastHits = new NativeList<RaycastHit>(Allocator.Persistent);

//            var raycastDependency = RaycastCommand.ScheduleBatch(raycastCommands, raycastHits.AsArray(), 32, setupDependency);
