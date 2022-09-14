using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Key : Item
{ 
    public Color keyColor;
    [SerializeField] MeshRenderer keyMesh;

    private void Start()
    {
        keyMesh.material.color = keyColor;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
         
            Inven.Inst.AddItem(Item_id);
            this.gameObject.SetActive(false);
        }
    }

}