using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;

public static class GoogleDriveTools
{
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
        GoogleDriveFiles.Create(file).Send().OnDone += json => { Debug.Log("json was uploaded"); };
    }

    public static void Update(string fileId, File newFile)
    {
        GoogleDriveFiles.Update(fileId, newFile).Send().OnDone += file => { Debug.Log("json was updated"); };
    }

    public static File Download(string fileId)
    {
        File output = new File();
        GoogleDriveFiles.Download(fileId).Send().OnDone += file => { output = file; };
        return output;
    }
}