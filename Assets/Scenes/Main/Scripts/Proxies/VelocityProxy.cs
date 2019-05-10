using Unity.Entities;
using UnityEngine;

namespace ECS
{
    [RequiresEntityConversion]
    public class VelocityProxy : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float Speed = 1f;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new Velocity { Value = Speed };
            dstManager.AddComponentData(entity, data);
        }
    }
}