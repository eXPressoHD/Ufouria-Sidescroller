using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{

    private AIPatrol _aiController;

    private void Start()
    {
        _aiController = GetComponentInParent<AIPatrol>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement pm = collision.GetComponent<PlayerMovement>();

        if (collision.gameObject.transform.GetChild(2).tag == "PlayerFeetAttackCollider")
        {
            pm.IsJumping = false;
            gameObject.GetComponentInParent<BoxCollider2D>().enabled = false;
            _aiController.TakeDamage(100);            
            pm.EnemyHitJump();
        }
    }
}
