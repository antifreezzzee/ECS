using Components.Interfaces;
using UnityEngine;

namespace Components
{
    public class RushAbility : MonoBehaviour, IAbility
    {
        public float rushForce;
        public float rushDelay;

        private float _rushTime = float.MinValue;

        public void Execute()
        {
            if (Time.time < _rushTime + rushDelay)
                return;
            _rushTime = Time.time;
            GetComponent<Rigidbody>().AddForce(transform.forward * rushForce, ForceMode.Impulse);
        }
    }
}