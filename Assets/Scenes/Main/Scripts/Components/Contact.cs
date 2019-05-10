﻿using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace ECS
{
    [Serializable]
    public struct Contact : IComponentData
    {
        public float3 PrevPos;
        public bool Value;
    }
}