using System.Collections.Generic;
using Components.Interfaces;
using UnityEngine;

namespace Components
{
    public class EnergyDrinkPickUpAbility : MonoBehaviour, ITargetedAbility
    {
        [SerializeField] private int time;
        [SerializeField] private float multiplier;
        public List<GameObject> Targets { get; set; } = new List<GameObject>();

        public void Execute()
        {
            foreach (var target in Targets)
            {
                var character = target.GetComponent<CharacterData>();
                if (character == null) return;
                character.IncreaseSpeed(time, multiplier);
                Destroy(gameObject);
            }
        }
    }
}