using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigid_player;
    Animator animator_player;
    Vector3 movement;

    public float turnSpeed;
    Quaternion rotation = Quaternion.identity;

    AudioSource AS_Foots;

    void Start()
    {
        animator_player = GetComponent<Animator>();
        rigid_player = GetComponent<Rigidbody>();
        AS_Foots = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal, 0, vertical);
        movement.Normalize(); 

        bool hasVerticalInput = !Mathf.Approximately(vertical, 0.0f);
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0.0f);

        bool isWalking = hasVerticalInput || hasHorizontalInput;
        animator_player.SetBool("IsWalking", isWalking);
        if (isWalking)
        {
            if (!AS_Foots.isPlaying)
            {
                AS_Foots.Play();
            }
        }
        else
            AS_Foots.Stop();

            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0.0f);
        rotation = Quaternion.LookRotation(desiredForward);
    }

    void Update()
    {
       
    }

    void OnAnimatorMove()
    {
        rigid_player.MovePosition(rigid_player.position + movement * animator_player.deltaPosition.magnitude);
        rigid_player.MoveRotation(rotation);

       
       
    }


}
