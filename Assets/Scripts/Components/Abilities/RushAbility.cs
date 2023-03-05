using Components.Interfaces;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class RushAbility : MonoBehaviour, IAbility
    {
        [SerializeField] private float rushForce;
        [SerializeField] private float rushDelay;

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