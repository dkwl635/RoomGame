using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvenSlot : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
     public int item_Id = -1;
    [SerializeField] private Image slotImg;
    [SerializeField] private Text itemInfoTxt;

    public static InvenSlot AddSlot(GameObject gameObject , Transform tr, int itemNum)
    {
        InvenSlot newSlot = GameObject.Instantiate(gameObject, tr).GetComponent<InvenSlot>();
        newSlot.SetSlot(itemNum);
        return newSlot;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item_Id <= 0)
            return;

        itemInfoTxt.transform.parent.gameObject.SetActive(true);
        itemInfoTxt.text = GameManager.Inst.ItemDates[item_Id].item_Name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoTxt.transform.parent.gameObject.SetActive(false);
    }

    public void SetSlot(int item_Id)
    {
        this.item_Id = item_Id;
        slotImg.sprite = GameManager.Inst.ItemDates[item_Id].item_Img;
        gameObject.SetActive(true);
    }

    public void UseItem()
    {
        Destroy(this.gameObject);
    }

}
