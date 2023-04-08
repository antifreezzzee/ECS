using Components.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Components.Behaviours
{
    public class AttackBehaviour : MonoBehaviour, IBehaviour
    {
        [SerializeField] private float attackDistance;
        private NavMeshAgent _navMeshAgent;
        private Transform _target;
        private ApplyDamageToTargetAbility _damageToTarget;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _target = FindObjectOfType<CharacterHealth>().transform;
            _damageToTarget = GetComponent<ApplyDamageToTargetAbility>();
            _damageToTarget.CanDamage = true;
        }

        public float Evaluate()
        {
            if (_target == null) return 0;
            if (Vector3.Distance(transform.position, _target.position) < attackDistance) return 1;
            return 0;
        }

        public void Behave()
        {
            _navMeshAgent.SetDestination(_target.position);
            Quaternion.RotateTowards(Quaternion.identity, _target.rotation, 0 );
        }
    }
}