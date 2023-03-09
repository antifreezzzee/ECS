using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class ShootCounter
    {
        [SerializeField] private double shootsCount;

        public static event Action OnShootsCountChanged;

        public double ShootsCount
        {
            get => shootsCount;
            set => shootsCount = value;
        }
        public void SaveStats()
        {
            var jsonString = JsonUtility.ToJson(this);
            PlayerPrefs.SetString("Stats", jsonString);
            OnShootsCountChanged?.Invoke();
            //GoogleDriveTools.Upload(jsonString);
        }
        public ShootCounter LoadStats()
        {
            if (PlayerPrefs.HasKey("Stats"))
                return JsonUtility.FromJson<ShootCounter>(PlayerPrefs.GetString("Stats"));
            return new ShootCounter();
        }
        public void InvokeChangedEvent()
        {
            OnShootsCountChanged?.Invoke();
        }
    }
}