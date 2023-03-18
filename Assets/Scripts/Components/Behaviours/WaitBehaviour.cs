using Components.Interfaces;
using UnityEngine;

namespace Components.Behaviours
{
    public class WaitBehaviour : MonoBehaviour, IBehaviour
    {
        [SerializeField] private float rotationSpeed;
        public float Evaluate()
        {
            return 0.5f;
        }

        public void Behave()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}