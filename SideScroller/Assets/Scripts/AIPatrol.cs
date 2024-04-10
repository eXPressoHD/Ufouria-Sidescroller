using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    [SerializeField]
    private float _health;

    [SerializeField]
    private float _walkSpeed;

    private bool _mustPatrol;
    private bool _mustTurn;

    private Rigidbody2D _rb;
    private Animator _anim;

    [SerializeField]
    private Transform _groundCheckPos;
    [SerializeField]
    private LayerMask _groundLayer;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _groundCheckPos = gameObject.transform.GetChild(0).GetComponent<Transform>();
        _mustPatrol = true;
    }

    private void FixedUpdate()
    {
        if(_mustPatrol)
        {
            _mustTurn = !Physics2D.OverlapCircle(_groundCheckPos.position, 0.1f, _groundLayer);
        }
    }

    private void Update()
    {
        if(_mustPatrol)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if(_mustTurn)
        {
            Flip();
        }

        _rb.velocity = new Vector2(_walkSpeed * Time.fixedDeltaTime, _rb.velocity.y);
    }

    private void Flip()
    {
        _mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        _walkSpeed *= -1;
        _mustPatrol = true;
    }    

    public void TakeDamage(float amount)
    {
        _health -= amount;

        if(_health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        _walkSpeed = 0f;
        _anim.SetBool("Died", true);        

        yield return new WaitForSeconds(.4f);

        Destroy(gameObject);
    }
}
