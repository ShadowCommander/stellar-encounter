using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECS
{
    [RequiresEntityConversion]
    public class BulletSpawnerProxy : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        public float FireRate;
        public GameObject Bullet;
        public float3 Offset;

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(Bullet);
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new BulletSpawner { FireRate = FireRate, Bullet = conversionSystem.GetPrimaryEntity(Bullet), Offset = Offset };
            dstManager.AddComponentData(entity, data);
        }
    }
}