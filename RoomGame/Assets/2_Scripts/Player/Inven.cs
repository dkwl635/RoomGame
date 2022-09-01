using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inven : MonoBehaviour
{
    public static Inven Inst;
    
    [SerializeField] Transform invenSlotTr;
    [SerializeField] private List<InvenSlot> invenSlots = new List<InvenSlot>();
    [SerializeField] GameObject slotObj;

    RectTransform rectTransform;

    [SerializeField] Button invenBtn;
   [SerializeField] bool Onoff = false;
    Vector2 currpos = Vector2.zero;
    Vector2 openPos = new Vector2(0, 0);
    Vector2 closePos = new Vector2(200, 0);
    [SerializeField] float durtime = 0.0f;
    private void Awake()
    {
        Inst = this;
        rectTransform = transform as RectTransform;
      
    }

    private void Start()
    {
        invenBtn.onClick.AddListener(OnOffInven);
        currpos = rectTransform.anchoredPosition;
    }

    private void Update()
    {
        if (Onoff)
        {
            currpos.x -= Time.deltaTime * 200;
            if (currpos.x <= 0.0f)
                return;
        }   
        else
        {
            currpos.x += Time.deltaTime * 200;
            if (currpos.x >= 200)
                return;
        } 
           
        rectTransform.anchoredPosition = currpos;
    }

    void OnOffInven()
    {
        currpos = rectTransform.anchoredPosition;
        Onoff = !Onoff;
      
    }

    public void AddItem(int item_Id)
    {
        invenSlots.Add(InvenSlot.AddSlot(slotObj, invenSlotTr, item_Id));
    }

    public bool FindItem(int item_Id)
    {
        return invenSlots.Find(item => item.item_Id == item_Id) == null ? false : true;
    }

    public void UseItem(int item_Id)
    {
        InvenSlot find = invenSlots.Find(item => item.item_Id == item_Id);
        if (find == null)
            return;

        invenSlots.Remove(find);
        find.UseItem();
    }

  


}