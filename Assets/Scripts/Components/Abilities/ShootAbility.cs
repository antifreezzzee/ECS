using Components.Interfaces;
using DefaultNamespace;
using UnityEngine;

namespace Components
{
    public class ShootAbility : MonoBehaviour, IAbility
    {
        [SerializeField] private Projectile bullet;
        [SerializeField] private float shootDelay;
        
        private float _shootTime = float.MinValue;
        public ShootCounter shootCounter;

        private void OnEnable()
        {
            GoogleDriveTools.OnFileDownloaded += GetRemoteStats;
        }

        private void OnDisable()
        {
            GoogleDriveTools.OnFileDownloaded -= GetRemoteStats;
        }
        private void Start()
        {
            GetLocalStats();
            GoogleDriveTools.Download(CharacterStatus.SaveFileId);
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
                var bulletTransform = transform;
                Instantiate(bullet, bulletTransform.position, bulletTransform.rotation)
                    .SetRicochet(CharacterStatus.IsRicochetBullets);
                shootCounter.ShootsCount++;
                shootCounter.InvokeChangedEvent();
                shootCounter.SaveStats();
            }
        }
    }
}