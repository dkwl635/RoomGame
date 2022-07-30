using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigid_player;
    Animator animator_player;
    Vector3 vec_Movement;

    public float turnSpeed;
    Quaternion rotation = Quaternion.identity;

    void Start()
    {
        animator_player = GetComponent<Animator>();
        rigid_player = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        vec_Movement = new Vector3(horizontal, 0, vertical);
        vec_Movement.Normalize(); 

        bool hasVerticalInput = !Mathf.Approximately(vertical, 0.0f);
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0.0f);

        bool isWalking = hasVerticalInput || hasHorizontalInput;
        animator_player.SetBool("IsWalking", isWalking);

   
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, vec_Movement, turnSpeed * Time.deltaTime, 0.0f);
        rotation = Quaternion.LookRotation(desiredForward);
    }

    void Update()
    {
       
    }

    void OnAnimatorMove()
    {
        rigid_player.MovePosition(rigid_player.position + vec_Movement  * animator_player.deltaPosition.magnitude);
        rigid_player.MoveRotation(rotation);
       
    }


}
