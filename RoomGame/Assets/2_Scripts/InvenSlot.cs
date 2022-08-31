using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvenSlot : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
     public int item_Id = -1;
    [HideInInspector] public bool empty = true;
    [SerializeField] private Image slotImg;
    [SerializeField] private Text itemInfoTxt;

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInfoTxt.transform.parent.gameObject.SetActive(true);
        itemInfoTxt.text = GameManager.Inst.ItemDates[item_Id].item_Name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoTxt.transform.parent.gameObject.SetActive(false);
    }

    public void SetSlot(int item_Id)
    {
        empty = false;
        this.item_Id = item_Id;
        slotImg.sprite = GameManager.Inst.ItemDates[item_Id].item_Img;
        gameObject.SetActive(true);
    }

    public void UseItem()
    {
        item_Id = -1;
        empty = true;
        this.gameObject.SetActive(false);
    }

}
