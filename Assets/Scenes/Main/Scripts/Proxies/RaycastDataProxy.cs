
using Unity.Entities;
using UnityEngine;

namespace ECS
{
    [RequiresEntityConversion]
    public class RaycastDataProxy : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new RaycastData { };
            dstManager.AddComponentData(entity, data);
        }
    }
}