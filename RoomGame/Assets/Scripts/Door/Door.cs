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


    bool opened = false;  //문이 열려있는지
    bool isPlayer = false;  //플레이어가 가까이 있는지
    bool isOpen = false;    //문을 열 수 있는지

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
                doorTxt.text = "E 키를 눌러 문을 여시요";
            else
                doorTxt.text = "문을 열 열쇠가 필요합니다.";

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
