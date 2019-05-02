using Unity.Entities;
using UnityEngine;

namespace ECS
{
    [RequiresEntityConversion]
    public class HealthProxy : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float Health = 20f;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new Health { Value = Health };
            dstManager.AddComponentData(entity, data);
        }
    }
}