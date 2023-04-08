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
        private GoogleDriveTools googleDriveTools = new GoogleDriveTools("GameData.json");

        public static event Action OnShootsCountChanged;
        
        public double ShootsCount
        {
            get => shootsCount;
            set => shootsCount = value;
        }

        public GoogleDriveTools GoogleDriveTools => googleDriveTools;

        public void InvokeChangedEvent()
        {
            OnShootsCountChanged?.Invoke();
        }


        public void SaveStats()
        {
            var jsonString = JsonUtility.ToJson(this);
            var jsonFile = googleDriveTools.CreateFileFromJson(jsonString);
            PlayerPrefs.SetString("Stats", jsonString);
            googleDriveTools.Update(CharacterStatus.SaveFileId, jsonFile);
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
            if (googleDriveTools.RemoteFile != null)
            {
                remoteFile = googleDriveTools.RemoteFile;
                //Debug.Log("json файл получен");
                byte[] remoteFileBytes = remoteFile.Content;
                //Debug.Log($"контент получен. количество байт: {remoteFile.Content.Length}");
                var remoteFileJsonString = Encoding.ASCII.GetString(remoteFileBytes);
                //Debug.Log($"json распарсен из байт: {remoteFileJsonString}");
                return JsonUtility.FromJson<ShootCounter>(remoteFileJsonString);
            }
            return new ShootCounter();
        }

    }
}