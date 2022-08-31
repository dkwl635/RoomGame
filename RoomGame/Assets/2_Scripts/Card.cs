using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Card : Item, IPointerEnterHandler , IPointerExitHandler
{
   enum State
    {
        Idle,
        OnPlayer,
    }
    State state;

    Renderer renderer;
    Material outline;

    GameObject E_txt;
  
    bool isPlayerSee = false;

    [SerializeField] float Distance;
    Transform playerTr;

    private void Start()
    {      
        renderer = GetComponent<Renderer>();
        outline = renderer.materials[1];
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        E_txt = GameObject.Find("E_Text");      
    }
    

    private void Update()
    {
        StateUpdate();
    }
    void StateUpdate()
    {
        switch (state)
        {
            case State.Idle:
                {
                    if (isPlayerSee)
                        if (Vector3.Distance(playerTr.position, transform.position) < Distance)
                            ChangeState(State.OnPlayer);
                }
                break;
            case State.OnPlayer:
                {
                    E_txt.transform.position = Camera.main.WorldToScreenPoint(transform.position);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        E_txt.SetActive(false);
                        Inven.Inst.AddItem(Item_id);
                        this.gameObject.SetActive(false);
                    }

                    if (Vector3.Distance(playerTr.position, transform.position) >= Distance)
                        ChangeState(State.Idle);

                }
                break;
           
        }
    }

    void ChangeState(State newState)
    {
        if (state == newState)
            return;

        state = newState;
        switch (state)
        {
            case State.Idle:
                {
                    outline.SetColor("_OutlineColor", Color.clear);
                    E_txt.SetActive(false);
                }
                break;
            case State.OnPlayer:
                {
                    outline.SetColor("_OutlineColor", Color.yellow);
                    E_txt.SetActive(true);

                }
                break;
        }


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPlayerSee = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPlayerSee = false;
        ChangeState(State.Idle);
    }

}
