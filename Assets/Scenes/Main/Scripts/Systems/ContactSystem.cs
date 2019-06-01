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
    //public class ContactSystem : JobComponentSystem
    //{
        //private static Unity.Physics.Systems.BuildPhysicsWorld physicsWorldSystem;
        //private static CollisionWorld collisionWorld;
        //protected static NativeArray<RaycastInput> rayCommands;
        //protected static NativeArray<RaycastHit> rayResults;

        //public void Start()
        //{
            //physicsWorldSystem = World.Active.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
            //collisionWorld = physicsWorldSystem.PhysicsWorld.CollisionWorld;
        //}

        //[BurstCompile]
        //public struct RaycastJob : IJobParallelFor
        //{
            //[ReadOnly] public CollisionWorld world;
            //[ReadOnly] public NativeArray<RaycastInput> inputs;
            //public NativeArray<RaycastHit> results;

            //public void Execute(int index)
            //{
                //world.CastRay(inputs[index], out RaycastHit hit);
                //results[index] = hit;
            //}
        //}

        //public static JobHandle ScheduleBatchRayCast(CollisionWorld world,
            //NativeArray<RaycastInput> inputs, NativeArray<RaycastHit> results)
        //{
            //JobHandle rcj = new RaycastJob
            //{
                //inputs = inputs,
                //results = results,
                //world = world

            //}.Schedule(inputs.Length, 5);
            //return rcj;
        //}

        //public static void SingleRayCast(CollisionWorld world, RaycastInput input,
//ref RaycastHit result)
        //{
            //rayCommands = new NativeArray<RaycastInput>(1, Allocator.Temp);
            //rayResults = new NativeArray<RaycastHit>(1, Allocator.Temp);
            //rayCommands[0] = input;
            //var handle = ScheduleBatchRayCast(world, rayCommands, rayResults);
            //handle.Complete();
            //result = rayResults[0];
            //rayCommands.Dispose();
            //rayResults.Dispose();
        //}

        //[BurstCompile]
        //public struct ContactJob : IJobForEachWithEntity<Translation, Rotation, Contact>
        //{
            //public float deltaTime;
            //public NativeList<RaycastHit> raycastHits;
            //[ReadOnly] public CollisionWorld world;

            //public void Execute(Entity entity, int index, [ReadOnly] ref Translation translation, [ReadOnly] ref Rotation rotation, ref Contact contact)
            //{
                //// Use raycast to check for collision with other entities.
                //// If found then set colliding to true and set entities list to colliding entities.
                //// Length of raycast is collider + speed

                //RaycastHit result = new RaycastHit();

                //SingleRayCast(collisionWorld, new RaycastInput() { Ray = new Ray() { Origin = contact.PrevPos, Direction = math.normalize(translation.Value - contact.PrevPos) } }, ref result);
                //if (result.RigidBodyIndex >= 0 && result.RigidBodyIndex < world.NumBodies)
                //{
                    //contact.Value = true;
                    //Debug.Log(result.ColliderKey);
                    //Debug.Log(result.Position);
                //}
            //}
        //}

        //// OnUpdate runs on the main thread.
        //protected override JobHandle OnUpdate(JobHandle inputDependencies)
        //{
            //var deltaTime = Time.deltaTime;

            //var job = new ContactJob()
            //{
                //deltaTime = Time.deltaTime
            //};

            //return job.Schedule(this, inputDependencies);
        //}
    //}
//}
