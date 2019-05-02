using Unity.Entities;
using UnityEngine;

namespace ECS
{
    [RequiresEntityConversion]
    public class ContactProxy : MonoBehaviour, IConvertGameObjectToEntity
    {
        public bool Value = false;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new Contact { Value = Value };
            dstManager.AddComponentData(entity, data);
        }
    }
}