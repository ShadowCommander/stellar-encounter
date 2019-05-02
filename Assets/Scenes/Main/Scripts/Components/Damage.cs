using System;
using Unity.Entities;

namespace ECS
{
    [Serializable]
    public struct Damage : IComponentData
    {
        public float Value;
    }
}