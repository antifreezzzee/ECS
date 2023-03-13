using System;
using System.Text;
using Components;
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
            var jsonFile = GoogleDriveTools.CreateFileFromJson(jsonString);
            PlayerPrefs.SetString("Stats", jsonString);
            GoogleDriveTools.Update(CharacterStatus.SaveFileId, jsonFile);
        }

        public ShootCounter LoadStats()
        {
            try
            {
                var remoteFile = GoogleDriveTools.Download(CharacterStatus.SaveFileId);
                var remoteFileBytes = remoteFile.Content;
                var remoteFileJsonString = Encoding.ASCII.GetString(remoteFileBytes);
                Debug.Log(remoteFileJsonString);
                return JsonUtility.FromJson<ShootCounter>(remoteFileJsonString);
                
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                if (PlayerPrefs.HasKey("Stats"))
                    return JsonUtility.FromJson<ShootCounter>(PlayerPrefs.GetString("Stats"));
            }

            return new ShootCounter();
        }

        public void InvokeChangedEvent()
        {
            OnShootsCountChanged?.Invoke();
        }
    }
}