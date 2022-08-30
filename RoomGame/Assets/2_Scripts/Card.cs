using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Card : Item, IPointerEnterHandler , IPointerExitHandler
{
    Renderer renderer;
    List<Material> materialList = new List<Material>();
    Material outline;

    GameObject E_txt;
  
    bool isPlayerSee = false;

    private void Start()
    {
        outline = GameManager.Inst.OutlineMaterials["CardOutline"];
        renderer = GetComponent<Renderer>();
        materialList.AddRange(renderer.materials);


        E_txt = GameObject.Find("E_Text");
       
    }
    private void Update()
    {
        if (isPlayerSee)
        {
            E_txt.transform.position = Camera.main.WorldToScreenPoint(transform.position);

            if (Input.GetKeyDown(KeyCode.E))
            {
                E_txt.SetActive(false);
                Inven.Inst.AddItem(Item_id);
                this.gameObject.SetActive(false);
            }
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {    
        E_txt.SetActive(true);
        isPlayerSee = true;
        materialList.Add(outline);
        renderer.materials = materialList.ToArray();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        E_txt.SetActive(false);
        isPlayerSee = false;
        materialList.Remove(outline);
        renderer.materials = materialList.ToArray();
    }

}
