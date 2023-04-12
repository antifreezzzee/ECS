using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;

public class GameConfiguration : MonoBehaviour
{
    [Header("Конфигурация по умолчанию:")] [SerializeField]
    private GameSettings defaultConfiguration;

    [Header("Текущая конфигурация:")] [SerializeField]
    private GameSettings _configuration;

    [Header("Значения текущей конфигурации:")] 
    [SerializeField] private string stringData;
    [SerializeField] private bool boolData;
    [SerializeField] private float floatData;

    public GameSettings Configuration
    {
        set => _configuration = value;
    }

    private string fileName = String.Empty;

    private bool CheckFileExist()
    {
        return File.Exists(fileName);
    }

    private void CreateDefaultConfig()
    {
        Debug.Log("Create file content");
        string fileContent = $"{defaultConfiguration.StringValue}" +
                             $"\n{defaultConfiguration.BoolValue.ToString()}" +
                             $"\n{defaultConfiguration.FloatValue.ToString()}";
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
        stringData = configs[0];
        boolData = Convert.ToBoolean(configs[1]);
        floatData = Convert.ToSingle(configs[2]);
        Debug.Log("Config filled");
    }

    private void Awake()
    {
        fileName = Application.persistentDataPath + "/config.txt";
        //Task.Run(LoadConfigFile);
    }

    public void ApplyConfiguration()
    {
        if (_configuration != null)
        {
            stringData = _configuration.StringValue;
            boolData = _configuration.BoolValue;
            floatData = _configuration.FloatValue;
        }
    }
}