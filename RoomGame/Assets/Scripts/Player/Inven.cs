using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inven : MonoBehaviour
{
    public static Inven Inst;
    [SerializeField] private InvenSlot[] invenSlots;
 
    private void Awake()
    {
        Inst = this;

        invenSlots = GetComponentsInChildren<InvenSlot>(true);
    }


    public void AddItem(int item_Id)
    {
        int idx = Array.FindIndex(invenSlots, x => x.empty);
        invenSlots[idx].SetSlot(item_Id);
    }

    public bool FindItem(int item_Id)
    {
        return Array.Exists(invenSlots, x => x.item_Id == item_Id);
    }

    public void UseItem(int item_Id)
    {
        Array.Find(invenSlots, x => x.item_Id == item_Id).UseItem();
    }

  


}