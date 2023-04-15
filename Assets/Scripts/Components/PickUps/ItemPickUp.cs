using System.Collections.Generic;
using Components.Interfaces;
using UnityEngine;

namespace Components
{
    public class ItemPickUp : PickUp, ITargetedAbility, IItem
    {
        [SerializeField] private GameObject uiItem;
        public List<GameObject> Targets { get; set; }
        public GameObject UIItem => uiItem;
        

        public void Execute()
        {
            foreach (var target in Targets)
            {
                var character = target.GetComponent<CharacterData>();
                if (character == null) return;
                var item = Instantiate(uiItem, character.InventoryUIRoot.transform, false);
                var ability = item.GetComponent<ITargetedAbility>();
                ability?.Targets.Add(target);
            }
            DestroyPickUp();
        }
    }
}