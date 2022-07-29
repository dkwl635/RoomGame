using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Vector3 vec_Movement;
    void Start()
    {
        
    }


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        vec_Movement = new Vector3(horizontal, 0, vertical);
        vec_Movement.Normalize();

        bool hasVerticalInput = !Mathf.Approximately(vertical, 0.0f);
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0.0f);

        bool isWalking = hasVerticalInput || hasHorizontalInput;

    }

}
