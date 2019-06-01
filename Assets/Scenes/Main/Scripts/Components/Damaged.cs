using System;
using Unity.Entities;

namespace ECS
{
    [Serializable]
    public struct Damaged : IComponentData
    {
        public float Value;
    }
}