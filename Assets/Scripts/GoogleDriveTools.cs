using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;

public static class GoogleDriveTools
{
    public static File RemoteFile;

    public static event Action OnFileDownloaded;

    public static List<File> FileList()
    {
        List<File> output = new List<File>();
        GoogleDriveFiles.List().Send().OnDone += fileList => { output = fileList.Files; };
        return output;
    }

    public static File CreateFileFromJson(string jsonString)
    {
        return new File {Name = "GameData.json", Content = Encoding.ASCII.GetBytes(jsonString)};
    }

    public static void Upload(File file)
    {
        GoogleDriveFiles.Create(file).Send().OnDone += json => { Debug.Log("json файл отправлен на сервер"); };
    }

    public static void Update(string fileId, File newFile)
    {
        GoogleDriveFiles.Update(fileId, newFile).Send().OnDone += file => { Debug.Log("json файл на сервере обновлен"); };
    }

    public static File Download(string fileId)
    {
        RemoteFile = new File();
        GoogleDriveFiles.Download(fileId).Send().OnDone += file => 
        { 
            Debug.Log("json файл скачан с сервера");
            RemoteFile = file; 
            OnFileDownloaded?.Invoke();
        };
        return RemoteFile;
    }
}