using System.Collections.Generic;
using Components.Interfaces;
using UnityEngine;

namespace Components
{
    public class ApplyDamageToTargetAbility : MonoBehaviour, ITargetedAbility
    {
        [SerializeField] private int damage;
        public List<GameObject> Targets { get; set; }
        public void Execute()
        {
            foreach (var target in Targets)
            {
                var health = target.GetComponent<CharacterHealth>();
                if (health != null)
                {
                    health.ReceiveDamage(damage);
                }
            }
        }
    }
}