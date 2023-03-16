using System;
using Components.Interfaces;
using UnityEngine;

namespace Components.Behaviours
{
    public class RotateBehaviour : MonoBehaviour, IBehaviour
    {
        [SerializeField] private CharacterHealth characterHealth;
        private void Start()
        {
            characterHealth = FindObjectOfType<CharacterHealth>();
        }

        public float Evaluate()
        {
            if (characterHealth == null) return 0;
            return 1/(gameObject.transform.position - characterHealth.transform.position).magnitude;
        }

        public void Behave()
        {
            transform.Rotate(Vector3.up, 10);
        }
    }
}