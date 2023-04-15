using System.Collections.Generic;
using Components.Interfaces;
using UnityEngine;

namespace Components
{
    public class RicochetPickUp : PickUp, ITargetedAbility
    {
        public List<GameObject> Targets { get; set; }

        public void Execute()
        {
            CharacterStatus.IsRicochetBullets = true;
            DestroyPickUp();
        }
    }
}