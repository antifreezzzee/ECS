using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        public float shootForce;
        public float selfDestroyDelay;

        private float _shootTime = float.MinValue;

        private void OnEnable()
        {
            GetComponent<Rigidbody>()
                .AddForce(transform.forward * shootForce, ForceMode.Impulse);
            _shootTime = Time.time;
        }

        private void Update()
        {
            checkSelfDestroy();
        }

        private void checkSelfDestroy()
        {
            if (Time.time < _shootTime + selfDestroyDelay)
                return;
            Destroy(gameObject);
        }
    }
}
