using Components;
using System;
using System.Text;
using UnityEngine;
using UnityGoogleDrive.Data;

namespace DefaultNamespace
{
    [Serializable]
    public class ShootCounter
    {
        [SerializeField] private double shootsCount;

        public static event Action OnShootsCountChanged;
        public void InvokeChangedEvent()
        {
            OnShootsCountChanged?.Invoke();
        }

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

        public ShootCounter LoadStatsFromPrefs()
        {
            if (PlayerPrefs.HasKey("Stats"))
                return JsonUtility.FromJson<ShootCounter>(PlayerPrefs.GetString("Stats"));
            return new ShootCounter();
        }

        public ShootCounter LoadStatsFromRemote()
        {
            File remoteFile = new File();
            if (GoogleDriveTools.RemoteFile != null)
            {
                remoteFile = GoogleDriveTools.RemoteFile;
                Debug.Log("json файл получен");
                byte[] remoteFileBytes = remoteFile.Content;
                Debug.Log($"контент получен. количество байт: {remoteFile.Content.Length}");
                var remoteFileJsonString = Encoding.ASCII.GetString(remoteFileBytes);
                Debug.Log($"json распарсен из байт: {remoteFileJsonString}");
                return JsonUtility.FromJson<ShootCounter>(remoteFileJsonString);
            }
            return new ShootCounter();
        }

    }
}