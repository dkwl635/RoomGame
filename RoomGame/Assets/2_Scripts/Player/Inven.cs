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

    [SerializeField] float durtime = 0.0f;
    [SerializeField] Text infoTxt;


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
            if (currpos.x > 0.0f)
                currpos.x -= Time.deltaTime * 200;
        }
        else
        {
            if (currpos.x < 200)
                currpos.x += Time.deltaTime * 200;
        } 
           
        rectTransform.anchoredPosition = currpos;

        if(durtime >= 0.0f)
        {
            durtime -= Time.deltaTime;
            if (durtime < 0.0f)
                infoTxt.gameObject.SetActive(false);
        }

    }

    void OnOffInven()
    {
        currpos = rectTransform.anchoredPosition;
        Onoff = !Onoff;
      
    }

    public void AddItem(int item_Id)
    {
        invenSlots.Add(InvenSlot.AddSlot(slotObj, invenSlotTr, item_Id));
        ShowTxtBox(GameManager.Inst.ItemDates[item_Id].item_Name + "¸¦ Å‰µæÇß½À´Ï´Ù.");
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

    void ShowTxtBox(string str)
    {
        infoTxt.gameObject.SetActive(true);
        infoTxt.text = str;
        durtime = 1.5f;

    }


}