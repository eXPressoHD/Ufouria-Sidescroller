using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField]
    private float _countDownSeconds;
    [SerializeField]
    private float _explodeRadius;

    private void Start()
    {
        StartCoroutine(BombExplosion());
    }

    public IEnumerator BombExplosion()
    {
        //Wait first for countdown to explosion and look whats inside the range after that
        yield return new WaitForSeconds(_countDownSeconds);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explodeRadius);

        foreach (Collider2D col in colliders)
        {
            if (col.tag == "BombDestroyable")
            {
                Destroy(col);
                col.gameObject.SetActive(false);
            }
            else if (col.tag == "Player")
            {
                PlayerHealthController pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthController>();

                if (pc != null)
                {
                    pc.TakeDamage(50);
                }
            }
            else if (col.tag == "Enemy")
            {
                AIPatrol aiController = col.GetComponent<AIPatrol>();

                if (aiController != null)
                {
                    aiController.TakeDamage(100);
                }
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, _explodeRadius);
    }
}
