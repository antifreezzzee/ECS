using System;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float shootForce;
        [SerializeField] private float selfDestroyDelay;

        private float _shootTime = float.MinValue;
        private bool _isRicochet;

        private void Update()
        {
            CheckSelfDestroy();
        }

        private void OnEnable()
        {
            GetComponent<Rigidbody>()
                .AddForce(transform.forward * shootForce, ForceMode.Impulse);
            _shootTime = Time.time;
        }

        private void CheckSelfDestroy()
        {
            if (Time.time < _shootTime + selfDestroyDelay)
                return;
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!_isRicochet)
            {
                Destroy(gameObject);
            }
        }

        public void SetRicochet(bool isRicochet)
        {
            _isRicochet = isRicochet;
        }
    }
}