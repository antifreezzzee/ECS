using Components;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class DisplayInfo : MonoBehaviour
    {
        [SerializeField] private Text shootsText;
        [SerializeField] private ShootAbility shootAbility;

        private void OnEnable()
        {
            ShootCounter.OnShootsCountChanged += UpdateText;
        }

        private void OnDisable()
        {
            ShootCounter.OnShootsCountChanged -= UpdateText;
        }

        private void UpdateText()
        {
            shootsText.text = $"Всего выстрелов: {shootAbility.shootCounter.ShootsCount}";
        }
    }
}