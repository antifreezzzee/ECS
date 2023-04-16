using DefaultNamespace;
using UnityEngine;

namespace Components.Interfaces
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private string description;

        public void SetDescription()
        {
            var textField = (InventoryItemDescription) FindObjectOfType(typeof(InventoryItemDescription));
            textField?.SetDescription(description);
        }
    }
}