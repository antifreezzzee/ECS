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

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Execute()
        {
            if (Time.time < _rushTime + rushDelay)
                return;
            _rushTime = Time.time;
            GetComponent<Rigidbody>().AddForce(transform.forward * rushForce, ForceMode.Impulse);
            _animator.SetBool("onRun", true);
            StartCoroutine(Show());
        }

        private IEnumerator Show()
        {
            meshRenderer.material = rushGlowMaterial;
            yield return new WaitForSeconds(rushDelay);
            meshRenderer.material = defaultMaterial;
            _animator.SetBool("onRun", false);
        }
    }
}