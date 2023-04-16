using System.Collections.Generic;
using Components.Interfaces;
using UnityEngine;

namespace Components
{
    public class GodModePickUpAbility : InventoryItem, ITargetedAbility
    {
        [SerializeField] private int time;
        public List<GameObject> Targets { get; set; } = new List<GameObject>();

        public void Execute()
        {
            foreach (var target in Targets)
            {
                var health = target.GetComponent<CharacterHealth>();
                if (health == null) return;
                health.SetGodMode(time);
                Destroy(gameObject);
            }
        }
    }
}