using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 150f;
    [SerializeField]
    private float _jumpForce = 250f;

    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private Transform _ceilingCheck;
    [SerializeField]
    private LayerMask _groundObjects;

    [SerializeField]
    private LayerMask _ladderObjects;

    private Rigidbody2D _rb;
    private Animator _anim;

    private float _horizontalMovement;
    private float _verticalMovement;
    [SerializeField]
    private float _distance;

    private bool _facingRight;
    private bool _isJumping = false;
    private bool _isGrounded;
    [SerializeField]
    private bool _isClimbing;

    [SerializeField]
    private float _checkRadius;

    //Prefabs
    [SerializeField]
    private GameObject _bomb;
    private bool _canUseBomb;

    public bool IsJumping
    {
        get { return _isJumping; }
        set { _isJumping = value; }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _canUseBomb = true;
        _facingRight = true;
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcessInputs();
        ProcessAnimation();
    }

    private void ProcessAnimation()
    {
        if (_horizontalMovement != 0)
        {
            _anim.SetFloat("Speed", 1);
        }
        else
        {
            _anim.SetFloat("Speed", 0);
        }

        if (_horizontalMovement > 0 && !_facingRight)
        {
            FlipCharacter();
        }
        else if (_horizontalMovement < 0 && _facingRight)
        {
            FlipCharacter();
        }
    }

    private void ProcessInputs()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            _isJumping = true;
        }

        if(Input.GetMouseButtonDown(0) && !_isJumping && _canUseBomb)
        {
            int direction = _facingRight ? 1 : -1;
            Instantiate(_bomb, transform.position + new Vector3(direction, -0.5f, 0), Quaternion.identity);
            StartCoroutine(BombUseCooldown(5f));
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontalMovement * _moveSpeed, _rb.velocity.y);

        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundObjects);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, _distance, _ladderObjects);

        if (hitInfo.collider != null)
        {
            if (Input.GetKey(KeyCode.W))
            {
                _isClimbing = true;
            }
        }
        else
        {
            _isClimbing = false;
        }

        if (_isClimbing)
        {
            _anim.SetBool("Climbing", true);
            _verticalMovement = Input.GetAxisRaw("Vertical");
            _rb.velocity = new Vector2(_rb.velocity.x, _verticalMovement * _moveSpeed);
            _rb.gravityScale = 0;
        }
        else
        {
            _anim.SetBool("Climbing", false);
            _rb.gravityScale = 2;
        }

        if (_isJumping && _isGrounded)
        {
            _rb.AddForce(new Vector2(0f, _jumpForce));
        }
        _isJumping = false;
    }

    private void FlipCharacter()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            _isJumping = false;
            transform.parent = col.transform;
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }

    public void EnemyHitJump()
    {
        _rb.AddForce(new Vector2(0f, 270));
    }

    private IEnumerator BombUseCooldown(float timeOfCooldown)
    {
        _canUseBomb = false;
        yield return new WaitForSeconds(timeOfCooldown);
        _canUseBomb = true;
    }
}
