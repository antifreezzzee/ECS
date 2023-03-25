using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public string StringValue;
        public bool BoolValue;
        public float FloatValue;
    }
}