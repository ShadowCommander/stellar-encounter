using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace ECS
{
    [Serializable]
    public struct RaycastData : IComponentData
    {
        public RaycastHit RaycastHit;
        public bool Hit;
    }
}