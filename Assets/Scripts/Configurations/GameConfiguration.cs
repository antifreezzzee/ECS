using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GameConfiguration : MonoBehaviour
{
    [SerializeField] private string defaultStringData;
    [SerializeField] private bool defaultBoolData;
    [SerializeField] private float defaultFloatData;

    private string _stringData;
    private bool _boolData;
    private float _floatData;

    private string fileName = String.Empty;
    public string StringData => _stringData;
    public bool BoolData => _boolData;
    public float FloatData => _floatData;

    private bool CheckFileExist()
    {
        return File.Exists(fileName);
    }

    private void CreateDefaultConfig()
    {
        Debug.Log("Create file content");
        string fileContent = $"{defaultStringData}\n{defaultBoolData.ToString()}\n{defaultFloatData.ToString()}";
        Debug.Log("Create file");
        StreamWriter streamWriter = new StreamWriter(fileName);
        Debug.Log("Fill file with content");
        streamWriter.WriteLine(fileContent);
        Debug.Log("Close stream");
        streamWriter.Close();
    }

    private void LoadConfigFile()
    {
        if (!CheckFileExist())
        {
            Debug.Log("File is not exist. Creating");
            CreateDefaultConfig();
        }
        Debug.Log("Thread sleep 10 sec... check freezes on start :)");
        Thread.Sleep(10000);
        StreamReader streamReader = new StreamReader(fileName);
        Debug.Log("Read file content");
        string fileContent = streamReader.ReadToEnd();
        Debug.Log("Close stream");
        streamReader.Close();
        Debug.Log("Create array");
        string[] configs = fileContent.Split('\n');
        Debug.Log("Fill config");
        _stringData = configs[0];
        _boolData = Convert.ToBoolean(configs[1]);
        _floatData = Convert.ToSingle(configs[2]);
        Debug.Log("Config filled");
    }

    private void Awake()
    {
        fileName = Application.persistentDataPath + "/config.txt";
        Task.Run(LoadConfigFile);
    }
    
    
}