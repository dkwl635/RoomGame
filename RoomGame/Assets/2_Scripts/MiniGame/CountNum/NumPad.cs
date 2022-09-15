using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NumPad : MonoBehaviour , IPointerClickHandler
{
    public static bool Push;

    public delegate bool PadPushEvent(int num);
    public PadPushEvent NumPadPush;


    public int num = 0;
    public Image img;
    public Text numTxt;

    public void OnEnable()
    {
        img.color = Color.white;
    }

    public void SetNum(int num)
    {
        this.num = num;
        numTxt.text = num.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Push)
            return;

        if (NumPadPush.Invoke(num))
            SetColor(Color.green);


    }

    public void SetColor(Color color)
    {
        img.color = color;
    }


}
