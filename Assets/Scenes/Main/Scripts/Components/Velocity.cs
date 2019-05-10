using System;
using Unity.Entities;

namespace ECS
{
    [Serializable]
    public struct Velocity : IComponentData
    {
        public float Value;
    }
}