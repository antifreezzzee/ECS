using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;

    public int Health => health;

    public void AddHealth(int count)
    {
        health += count;
    }

    public void GetDamage(int count)
    {
        if (health > 0)
            health -= count;
        if (health < 0)
            health = 0;
    }
}