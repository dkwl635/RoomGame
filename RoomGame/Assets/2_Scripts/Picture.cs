using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Picture : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler
{
    Renderer renderer;
    List<Material> materialList = new List<Material>();
    Material outline;

    GameObject E_txt;
    bool isPlayerSee = false;

    private void Awake()
    {
        E_txt = GameObject.Find("E_Text");
    }

    private void Start()
    {
        outline = GameManager.Inst.OutlineMaterials["PictureOutline"];
        renderer = GetComponent<Renderer>();
        materialList.AddRange(renderer.materials);
    }
    private void Update()
    {
        if (isPlayerSee)
        {
            E_txt.transform.position = Camera.main.WorldToScreenPoint(transform.position);

            if (Input.GetKeyDown(KeyCode.E))
            {
                E_txt.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {      
        isPlayerSee = true;
        materialList.Add(outline);
        renderer.materials = materialList.ToArray();
    }

    public void OnPointerExit(PointerEventData eventData)
    {     
        isPlayerSee = false;
        E_txt.SetActive(false);
        materialList.Remove(outline);
        renderer.materials = materialList.ToArray();
    }
}
