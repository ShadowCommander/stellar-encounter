using Unity.Entities;
using UnityEngine;

namespace ECS
{
    [RequiresEntityConversion]
    public class DamageProxy : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float Damage = 5f;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new Damage { Value = Damage };
            dstManager.AddComponentData(entity, data);
        }
    }
}