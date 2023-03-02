using UnityEngine;

namespace Components
{
    public class Projectile : MonoBehaviour
    {
        public float shootForce;

        private void OnEnable()
        {
            GetComponent<Rigidbody>()
                .AddForce(transform.forward * shootForce, ForceMode.Impulse);
        }
    }
}