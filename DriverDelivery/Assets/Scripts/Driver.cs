using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] private float steerSpeed = 1f;
    [SerializeField] private float moveSpeed = 0.01f;
    [SerializeField] private float slowSpeed = 0.01f;
    [SerializeField] private float boostSpeed = 0.01f;

    private string BOOST_TAG = "Boost";
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0,moveAmount,0);
    }

    private void OnCollisionEnter2D(Collision2D collison)
    {
        moveSpeed = slowSpeed;
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == BOOST_TAG)
        {
            moveSpeed = boostSpeed;
        }
    }
}
