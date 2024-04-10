using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField]
    private float _limitRight;
    [SerializeField]
    private float _limitLeft;

    [SerializeField]
    private float _speed;

    private int direction = 1;

    private void FixedUpdate()
    {
        if (transform.position.x > _limitRight)
        {
            direction = -1;
        }
        else if (transform.position.x < _limitLeft)
        {
            direction = 1;
        }

        var movement = Vector3.right * direction * _speed * Time.deltaTime;
        transform.Translate(movement);
    }    
}
