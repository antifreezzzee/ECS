using Components.Interfaces;
using DefaultNamespace;
using UnityEngine;

namespace Components
{
    public class ShootAbility : MonoBehaviour, IAbility
    {
        [SerializeField] private Projectile bullet;
        [SerializeField] private float shootDelay;
        [SerializeField] private Transform bulletEmitter;
        
        private float _shootTime = float.MinValue;
        public ShootCounter shootCounter;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            shootCounter.GoogleDriveTools.OnFileDownloaded += GetRemoteStats;
        }

        private void OnDisable()
        {
            shootCounter.GoogleDriveTools.OnFileDownloaded -= GetRemoteStats;
        }
        private void Start()
        {
            GetLocalStats();
            shootCounter.GoogleDriveTools.Download(CharacterStatus.SaveFileId);
        }

        private void GetRemoteStats()
        {
            shootCounter = new ShootCounter();
            shootCounter = shootCounter.LoadStatsFromRemote();
            shootCounter.InvokeChangedEvent();
        }

        private void GetLocalStats()
        {
            shootCounter = new ShootCounter();
            shootCounter = shootCounter.LoadStatsFromPrefs();
            shootCounter.InvokeChangedEvent();
        }

        public void Execute()
        {
            if (Time.time < _shootTime + shootDelay)
                return;
            _shootTime = Time.time;
            if (bullet != null)
            {
                _animator.SetTrigger("Shoot");
                var bulletTransform = bulletEmitter;
                Instantiate(bullet, bulletTransform.position, bulletTransform.rotation)
                    .SetRicochet(CharacterStatus.IsRicochetBullets);
                shootCounter.ShootsCount++;
                shootCounter.InvokeChangedEvent();
                shootCounter.SaveStats();
            }
        }
    }
}