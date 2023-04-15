using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private CharacterHealth _characterHealth;

    private void Awake()
    {
        HideInventory();
    }

    private void Update()
    {
        if (!_characterHealth.IsAlive)
            HideInventory();
    }

    public void ShowInventory()
    {
        if (_characterHealth.IsAlive)
            inventoryCanvas.SetActive(true);
    }

    public void HideInventory()
    {
        inventoryCanvas.SetActive(false);
    }
}