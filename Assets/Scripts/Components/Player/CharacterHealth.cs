using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int godTime;
    private Animator _animator;
    private bool _isAlive;
    private bool _isGodMode;
    public bool IsAlive => _isAlive;
    public bool IsGodMode => _isGodMode;

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
            if (_isGodMode)
                return;
            SetGodMode(godTime);
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

    public async void SetGodMode(int time)
    {
        if (!_isGodMode)
        {
            _isGodMode = true;
            await Task.Run((() => Thread.Sleep(time * 1000)));
            _isGodMode = false;
        }
    }
}