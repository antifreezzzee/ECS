using System.Collections.Generic;
using Components.Interfaces;
using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class BehaviourManager : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private List<MonoBehaviour> behaviours;

        public IBehaviour ActiveBehaviour { get; set; }

        public List<MonoBehaviour> Behaviours => behaviours;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent<AIAgent>(entity);
        }
    }

    public struct AIAgent : IComponentData
    {
    }
}