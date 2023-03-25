using System;
using DefaultNamespace;
using UnityEngine;
using Zenject;

public class MyInstaller : MonoInstaller
{
    [SerializeField] private SettingsType settingsType;
    [SerializeField] private GameSettings scriptableObjectSettings;
    [SerializeField] private DummyGameSettings monoBehaviourSettings;

    private enum SettingsType
    {
        ScriptableObject,
        DummyScript
    }

    public override void InstallBindings()
    {
        switch (settingsType)
        {
            case SettingsType.DummyScript:
                Container.Bind<DummyGameSettings>().AsSingle().NonLazy();
                Debug.Log("DummyInjected");
                break;
            case SettingsType.ScriptableObject:
                Container.Bind<GameSettings>().AsSingle().NonLazy();
                Debug.Log("ScriptableObjectInjected");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}