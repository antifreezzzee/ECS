using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class InventoryItemDescription : MonoBehaviour
    {
        [SerializeField] private Text descriptionText;
        [SerializeField] private string defaultText;

        private void Awake()
        {
            descriptionText.text = defaultText;
        }

        private void OnDisable()
        {
            descriptionText.text = defaultText;
        }

        public void SetDescription(string text)
        {
            descriptionText.text = text;
        }
    }
}