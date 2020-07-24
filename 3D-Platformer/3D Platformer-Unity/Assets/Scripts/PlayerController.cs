﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; // This will be set inside the Unity inspector for this object
    public float jumpForce;

    private Rigidbody rig;


    void Awake () 
    {
        // Get the rigidbody component
        rig = GetComponent<Rigidbody>();
    }

    void Update () // Called every frame
    {
        Move(); // Every frame we're calling `move`

        if(Input.GetButtonDown("Jump"))
        {
            TryJump();
        }
    }

    void Move () // To move the player
    {
        // Getting inputs; we'll then apply this to our velocity
        float xInput = Input.GetAxis("Horizontal"); // Unity input method, horizontal axis /#/ x & y coords // If no button, it is set to 0
        float zInput = Input.GetAxis("Vertical"); // Unity transform/position = x, y, z; x = x, z = y

        // Velocity
        Vector3 dir = new Vector3(xInput, 0, zInput) * moveSpeed;
        rig.velocity = dir;
        // Manually set y direction 
        dir.y = rig.velocity.y; // whatever the existing velocity is

        Vector3 facingDir = new Vector3(xInput, 0, zInput); 
        if(facingDir.magnitude > 0)
        {
            transform.forward = facingDir;
        }
    }

    void TryJump () // We don't know if we are able to jump yet, hence the name "TryJump'
    {
        Ray ray1 = new Ray(transform.position + new Vector3(0.5f, 0, 0.5f), Vector3.down);
        Ray ray2 = new Ray(transform.position + new Vector3(-0.5f, 0, 0.5f), Vector3.down);
        Ray ray3 = new Ray(transform.position + new Vector3(0.5f, 0, -0.5f), Vector3.down);
        Ray ray4 = new Ray(transform.position + new Vector3(-0.5f, 0, -0.5f), Vector3.down);

        // Shoot the raycast
        bool raycast1 = Physics.Raycast(ray1, 0.7f); // true/false
        bool raycast2 = Physics.Raycast(ray2, 0.7f);
        bool raycast3 = Physics.Raycast(ray3, 0.7f);
        bool raycast4 = Physics.Raycast(ray4, 0.7f);

        if(raycast1 || raycast2 || raycast3 || raycast4)
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Adding force upwards
        }

        //if(Physics.Raycast(ray, 0.7f))
        //{
          //  rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Adding force upwards
        //}
    }
}
