using System.Collections.Generic;
using Components.Interfaces;
using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class DOTWeenAbility : MonoBehaviour, IConvertGameObjectToEntity, IAbility
    {
        [SerializeField] private List<MonoBehaviour> DOTWeenActions;

        private List<IDOTWeenAbility> DOTWeenActionAbilities = new List<IDOTWeenAbility>();

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new DOTWeenData());
        }

        private void Start()
        {
            foreach (var action in DOTWeenActions)
            {
                if (action is IDOTWeenAbility dotWeenAbility)
                    DOTWeenActionAbilities.Add(dotWeenAbility);
                else
                    Debug.LogError("DOTWeen action must derive from IDOTWeenAbility");
            }
        }

        public void Execute()
        {
            foreach (var action in DOTWeenActionAbilities)
            {
                action.Execute();
            }
        }

        public struct DOTWeenData : IComponentData
        {
        }
    }
}