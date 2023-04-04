using System.Collections.Generic;
using Components.Interfaces;
using UnityEngine;

namespace Components
{
    public class RicochetPickUp : MonoBehaviour, ITargetedAbility
    {
        [SerializeField] private GameObject model;
        public void Execute()
        {
            CharacterStatus.IsRicochetBullets = true;
            //Destroy(gameObject);
            model.SetActive(false);
        }

        public List<GameObject> Targets { get; set; }
    }
}