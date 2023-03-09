using System;
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

    public static void Upload(String str)
    {
        var file = new File {Name = "GameData.json", Content = Encoding.ASCII.GetBytes(str)};
        GoogleDriveFiles.Create(file).Send().OnDone += json => { Debug.Log("json was uploaded"); };
    }

    public static File Download(String fileId)
    {
        File output = new File();
        GoogleDriveFiles.Download(fileId).Send().OnDone += file => { output = file; };
        return output;
    }
}