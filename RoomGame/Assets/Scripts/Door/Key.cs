using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Key : MonoBehaviour,  IPointerExitHandler ,IPointerEnterHandler, IPointerClickHandler
{
    public int keyNum;
    public Color keyColor;
    [SerializeField] MeshRenderer keyMesh;



    private void Start()
    {
        keyMesh.material.color = keyColor;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("마우스 Exit");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("마우스 On");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("마우스 click");
    }
}