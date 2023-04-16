using System;
using Components.Interfaces;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public class DOTWeenMoveAbility : MonoBehaviour, IDOTWeenAbility
    {
        [SerializeField] private float height;
        [SerializeField] private float speed;

        private ApplyDamageToTargetAbility _damageToTarget;
        private bool onMove = false;
        private bool moveDown = false;
        private Vector3 lastPosition = Vector3.zero;
        private Vector3 currentPosition = Vector3.zero;


        private void Awake()
        {
            _damageToTarget = GetComponent<ApplyDamageToTargetAbility>();
            lastPosition = transform.position;
        }

        public void Execute()
        {
            if (!onMove)
                doMove();
        }

        private void doMove()
        {
            onMove = true;
            transform.DOMoveY(height, speed).SetLoops(-1, LoopType.Yoyo);
        }

        private void FixedUpdate()
        {
            currentPosition = transform.position;
            if (currentPosition.y < lastPosition.y) moveDown = true;
            else moveDown = false;
            lastPosition = transform.position;
            _damageToTarget.CanDamage = moveDown;
        }
    }
}