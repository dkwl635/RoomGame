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
    bool isRot = false;

    float rotTime = 0.0f;
    [SerializeField] Quaternion origin;
    [SerializeField] Quaternion next;

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
                PictureRot();
                E_txt.SetActive(false);
              
            }
        }

        if(isRot)
        {
            rotTime += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(origin, next, rotTime);
            if(rotTime >= 1.0f)
            {
                isRot = false;
                transform.rotation = next;          
            }

        }


    }


    public void OnPointerEnter(PointerEventData eventData)
    {      
        isPlayerSee = true;
        E_txt.SetActive(true);
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

    void PictureRot()
    {
        origin = transform.rotation;
        next = origin * Quaternion.Euler(0, 0, 90.0f);
        rotTime = 0.0f;
        isRot = true;
    }
   

}
