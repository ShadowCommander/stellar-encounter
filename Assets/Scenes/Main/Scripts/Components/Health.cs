using System;
using Unity.Entities;

namespace ECS
{
    [Serializable]
    public struct Health : IComponentData
    {
        public float Value;
    }
}