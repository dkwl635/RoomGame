using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Door : MonoBehaviour
{
    public GameObject doorTxtObj;
    public Collider doorCollider;
    Animation doorAnim;
    Text doorTxt;


    bool opened = false;  //���� �����ִ���
    bool isPlayer = false;  //�÷��̾ ������ �ִ���
    bool isOpen = false;    //���� �� �� �ִ���

    public bool IsOpen { set { isOpen = value; } }

    private void Awake()
    {
        doorAnim = GetComponentInChildren<Animation>();

    }
    private void Start()
    {
        doorTxt = doorTxtObj.GetComponent<Text>();
    }

    public void OpenDoor()
    {
        if (!opened)
        {
            opened = true;
            doorCollider.enabled = false;
            doorTxtObj.SetActive(false);
            doorAnim.Play();
        }
    }

    private void Update()
    {
        if (isOpen && isPlayer && Input.GetKeyDown(KeyCode.E))
            OpenDoor();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !opened)
        {
            isPlayer = true;
            doorTxtObj.SetActive(true);

            if (isOpen)
                doorTxt.text = "E Ű�� ���� ���� ���ÿ�";
            else
                doorTxt.text = "���� �� ���谡 �ʿ��մϴ�.";

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
