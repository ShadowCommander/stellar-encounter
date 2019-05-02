using System;
using Unity.Entities;
using Unity.Mathematics;

namespace ECS
{
    [Serializable]
    public struct BulletSpawner : IComponentData
    {
        public float FireRate;
        public float Timer;
        public Entity Bullet;
        public float3 Offset;
    }
}