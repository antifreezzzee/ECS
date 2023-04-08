using System;
using System.Text;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;

public class GoogleDriveTools
{
    private File _remoteFile;
    private string _fileName;
    public File RemoteFile => _remoteFile;
    public event Action OnFileDownloaded;

    public GoogleDriveTools(string fileName)
    {
        _fileName = fileName;
    }
    
    public File CreateFileFromJson(string jsonString)
    {
        return new File {Name = _fileName, Content = Encoding.ASCII.GetBytes(jsonString)};
    }
    public void Upload(File file)
    {
        GoogleDriveFiles.Create(file).Send().OnDone += json =>
        {
            /*Debug.Log("json файл отправлен на сервер");*/
        };
    }
    public void Update(string fileId, File newFile)
    {
        GoogleDriveFiles.Update(fileId, newFile).Send().OnDone += file =>
        {
            /*Debug.Log("json файл на сервере обновлен");*/
        };
    }
    public void Download(string fileId)
    {
        _remoteFile = new File();
        GoogleDriveFiles.Download(fileId).Send().OnDone += file =>
        {
            //Debug.Log("json файл скачан с сервера");
            _remoteFile = file;
            OnFileDownloaded?.Invoke();
        };
    }
}