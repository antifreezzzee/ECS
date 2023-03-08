using Components.Interfaces;
using UnityEngine;

namespace Components
{
    public class ShootAbility : MonoBehaviour, IAbility
    {
        [SerializeField] private Projectile bullet;
        [SerializeField] private float shootDelay;

        private float _shootTime = float.MinValue;

        public void Execute()
        {
            if (Time.time < _shootTime + shootDelay)
                return;
            _shootTime = Time.time;
            if (bullet != null)
            {
                var bulletTransform = transform;
                Instantiate(bullet, bulletTransform.position, bulletTransform.rotation).
                    SetRicochet(CharacterStatus.IsRicochetBullets);
            }
        }
    }
}