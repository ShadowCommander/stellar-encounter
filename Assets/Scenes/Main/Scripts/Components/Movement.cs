using System;
using Unity.Entities;

namespace ECS
{
    [Serializable]
    public struct Movement : IComponentData
    {
        public float Value;
    }
}