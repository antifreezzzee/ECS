using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Components.Interfaces;
using UnityEngine;

namespace Components
{
    public class CharacterData : MonoBehaviour
    {
        [SerializeField] private float defaultMoveSpeed;
        [SerializeField] private GameObject inventoryUIRoot;
        private List<IItem> _items;
        private float _moveSpeed;
        public float MoveSpeed => _moveSpeed;
        public GameObject InventoryUIRoot => inventoryUIRoot;

        private void Awake()
        {
            _moveSpeed = defaultMoveSpeed;
        }

        public async void IncreaseSpeed(int time, float multiplier)
        {
            _moveSpeed *= multiplier;
            await Task.Run(() => Thread.Sleep(time * 1000));
            _moveSpeed = defaultMoveSpeed;
        }
    }
}