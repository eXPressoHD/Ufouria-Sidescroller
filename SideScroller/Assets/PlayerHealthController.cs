using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField]
    private int _playerHealth;
    private PlayerHealthBar _phb;
    private PlayerMovement _pm;

    private void Start()
    {
        _pm = GetComponent<PlayerMovement>();
        _phb = GetComponent<PlayerHealthBar>();
    }

    public void TakeDamage(int amount)
    {
        _playerHealth -= amount;

        _phb.Deduct(amount);

        _pm.EnemyHitJump(); //TODO unsichtbarer collider kurz oder doch jump

        if (_playerHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
