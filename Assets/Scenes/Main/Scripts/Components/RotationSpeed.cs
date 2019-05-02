using System;
using Unity.Entities;

namespace ECS
{
    [Serializable]
    public struct RotationSpeed : IComponentData
    {
        public float RadiansPerSecond;
    }
}