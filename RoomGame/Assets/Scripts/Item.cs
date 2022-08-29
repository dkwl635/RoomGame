using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int Item_id;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Inven.Inst.AddItem(Item_id);
            this.gameObject.SetActive(false);
        }
    }
}
