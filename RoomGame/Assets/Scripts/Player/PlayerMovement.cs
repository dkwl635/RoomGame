using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigid;
    Animator animator;
    Vector3 movement;
     
    public float turnSpeed; //3��Ī �̵��ӵ�
  
    Quaternion rotation = Quaternion.identity;

    AudioSource footsAudio;

    bool isKeyMove =true;

    //1��Ī
    [Header("FirstPersonView")]
    [SerializeField] GameObject camera;
    [SerializeField] float moveSpeed; //1��Ī �̵��ӵ�
    [SerializeField] float rotSpeed = 200.0f;
    float mx,my; //���콺 �̵� ����
    float camX, y; //�̵� ����
  



    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        footsAudio = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mx = Input.GetAxis("Mouse X"); //���콺 �̵� �ޱ�
            my = Input.GetAxis("Mouse Y"); //���콺 �̵� �ޱ�

            float _cameraRotationX = my * rotSpeed;

            camX -= _cameraRotationX;
            camX = Mathf.Clamp(camX, -20, 20); //�ִ� �ּ� ����

            camera.transform.localEulerAngles = new Vector3(camX, 0f, 0f);

            //ĳ���� ȸ��
            Vector3 _characterRotationY = new Vector3(0f, mx, 0f) * rotSpeed;
            rigid.MoveRotation(rigid.rotation * Quaternion.Euler(_characterRotationY)); // ���ʹϾ� * ���ʹϾ�
        }
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal"); //x
        float vertical = Input.GetAxis("Vertical");        //z

        movement = new Vector3(horizontal, 0, vertical);
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0.0f); //0.......�� �ټ��� ���̸� üũ�ϱ� ����
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0.0f);

        bool isWalking = hasVerticalInput || hasHorizontalInput; //�������� �ִ���

        animator.SetBool("IsWalking", isWalking);
        if (isWalking)        
           if (!footsAudio.isPlaying)      
                footsAudio.Play();        
        else
            footsAudio.Stop();

        if (isKeyMove)
        {       
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0.0f);
            rotation = Quaternion.LookRotation(desiredForward);    
        }
        else
        {
            Vector3 _moveHorizontal = transform.right * horizontal;
            Vector3 _moveVertical = transform.forward * vertical;
            Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * moveSpeed;

            rigid.MovePosition(transform.position + _velocity * Time.deltaTime);
        }
    }

    private void OnAnimatorMove()
    {
        if (!isKeyMove)
            return;

        rigid.MovePosition(rigid.position + movement * animator.deltaPosition.magnitude);
        rigid.MoveRotation(rotation);
    }

    public void ChangeMoveMode()
    {
        isKeyMove = !isKeyMove;
    }


}
