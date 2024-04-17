using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    Animator animator;
    [SerializeField] private float _jumpForce = 200;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), rb.velocity.y, Input.GetAxis("Vertical"));
        
        rb.velocity = dir * _speed;
        
        if (Input.GetKeyDown(KeyCode.Space)) rb.AddForce(Vector3.up * _jumpForce);
    }
}
