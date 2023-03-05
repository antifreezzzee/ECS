using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;

    public int Health
    {
        get => health;
        set => health = value;
    }
}