using System.Collections;
using Components.Interfaces;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class RushAbility : MonoBehaviour, IAbility
    {
        [SerializeField] private float rushForce;
        [SerializeField] private float rushDelay;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material rushGlowMaterial;
        [SerializeField] private SkinnedMeshRenderer meshRenderer;

        private float _rushTime = float.MinValue;

        public void Execute()
        {
            if (Time.time < _rushTime + rushDelay)
                return;
            _rushTime = Time.time;
            GetComponent<Rigidbody>().AddForce(transform.forward * rushForce, ForceMode.Impulse);
            StartCoroutine(ChangeMaterial());
        }

        private IEnumerator ChangeMaterial()
        {
            meshRenderer.material = rushGlowMaterial;
            yield return new WaitForSeconds(2);
            meshRenderer.material = defaultMaterial;
        }
    }
}