using System;
using Unity.Entities;

namespace ECS
{
    [Serializable]
    public struct Contact : IComponentData
    {
        public bool Value;
    }
}