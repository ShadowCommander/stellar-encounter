using Unity.Entities;
using UnityEngine;

namespace ECS
{
    [RequiresEntityConversion]
    public class MovementProxy : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float Speed = 1f;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new Movement { Value = Speed };
            dstManager.AddComponentData(entity, data);
        }
    }
}