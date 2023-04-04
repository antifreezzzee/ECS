using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float godTime;
    private double _damageTime = double.MinValue;
    private Animator _animator;
    private bool _isAlive;
    public bool IsAlive => _isAlive;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _isAlive = true;
    }

    public void AddHealth(int count)
    {
        if (_isAlive)
            health += count;
    }

    public void ReceiveDamage(int count)
    {
        if (_isAlive)
        {
            if (Time.time < _damageTime + godTime)
                return;
            _damageTime = Time.time;
            health -= count;
            if (health <= 0)
                Die();
            else _animator.SetTrigger("Hit");
        }
    }
    private void Die()
    {
        health = 0;
        _isAlive = false;
        _animator.SetTrigger("Die");
    }
}