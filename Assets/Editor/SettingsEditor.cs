using UnityEditor;

namespace DefaultNamespace
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(GameConfiguration))]
    public class SettingsEditor : Editor
    {
        private string[] _settingsArray;
        private string[] _settingsNamesArray;
        private int _selectedSettingIndex;
        private GameSettings _gameSettingsPreset;
        private bool _applySettings;

        private void OnEnable()
        {
            _settingsArray = AssetDatabase.FindAssets("GameSettingsPreset");
            ApplySettings();
            _settingsNamesArray = new string[_settingsArray.Length];
            for (int i = 0; i < _settingsArray.Length; i++)
            {
                var fullPath = AssetDatabase.GUIDToAssetPath(_settingsArray[i]);
                var fullPathSplit = fullPath.Split('/');
                _settingsNamesArray[i] = fullPathSplit[fullPathSplit.Length - 1];
            }
        }

        public override void OnInspectorGUI()
        {
            _selectedSettingIndex =
                EditorGUILayout.Popup("Выбор настроек:", _selectedSettingIndex, _settingsNamesArray);
            ApplySettings();
            base.OnInspectorGUI();
        }

        private void ApplySettings()
        {
            var path = AssetDatabase.GUIDToAssetPath(_settingsArray[_selectedSettingIndex]);
            _gameSettingsPreset = (GameSettings) AssetDatabase.LoadAssetAtPath(path, typeof(GameSettings));
            GameConfiguration gameConfiguration = (GameConfiguration) target;
            gameConfiguration.Configuration = _gameSettingsPreset;
            gameConfiguration.ApplyConfiguration();
        }
    }
}