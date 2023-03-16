using Components.Interfaces;
using UnityEngine;

namespace Components.Behaviours
{
    public class WaitBehaviour : MonoBehaviour, IBehaviour
    {
        public float Evaluate()
        {
            return 0.5f;
        }

        public void Behave()
        {
            Debug.Log("waiting");
        }
    }
}