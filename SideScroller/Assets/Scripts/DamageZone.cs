using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField]
    private int _damageOnTouch;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerHealthController phc = collision.gameObject.GetComponent<PlayerHealthController>();
            phc.TakeDamage(_damageOnTouch);
        }
    }
}
