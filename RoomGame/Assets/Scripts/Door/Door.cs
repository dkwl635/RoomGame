using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Door : MonoBehaviour
{
    public int  doorNum = 0;
    [SerializeField] Color doorColor;
    [SerializeField] MeshRenderer doorMark;
    [SerializeField] GameObject doorTxtObj;
    [SerializeField] Collider doorCollider;
    Animation doorAnim;
    Text doorTxt;


    bool opened = false;  //���� �����ִ���
    bool isPlayer = false;  //�÷��̾ ������ �ִ���
    [SerializeField] bool isOpen = false;    //���� �� �� �ִ���



    private void Awake()
    {
        doorAnim = GetComponentInChildren<Animation>();

    }
    private void Start()
    {
        doorTxt = doorTxtObj.GetComponent<Text>();
        if (doorMark)
            doorMark.material.color = doorColor;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }                
    }

    public void CheckDoor(bool getKey)
    {
        if(getKey)
        {
            isOpen = true;
            doorTxt.text = "E Ű�� ���� ���� ���ÿ�";
        }    
        else
        {
            doorTxt.text = "���� �� ���谡 �ʿ��մϴ�.";
            isOpen = false;
        }
    }
        

     void OpenDoor()
    {
        if (!opened && isOpen && isPlayer)
        {        
            opened = true;
            doorTxtObj.SetActive(false);
            doorAnim.Play("Door_Open");

            Invoke("OffCollider", 1.0f);
        }
        else if (!isOpen && isPlayer)
        {
            doorAnim.Play("Door_Jam");
        }
    }

    void OffCollider()
    {
        doorCollider.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !opened)
        {      
            isPlayer = true;
            doorTxtObj.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayer = false;
            doorTxtObj.SetActive(false);
        }
    }

}
