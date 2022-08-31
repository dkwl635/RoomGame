using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Picture : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler
{
    enum State
    {
        Idle,
        OnPlayer,
        Rot,
    }

    [SerializeField] State state;

    Renderer renderer;  
    Material outline;

    GameObject E_txt;
    bool isPlayerSee = false;

    float rotTime = 0.0f;
    [SerializeField] Quaternion origin;
    [SerializeField] Quaternion next;

    [SerializeField]float Distance;
    Transform playerTr;

    public int num
    {
        get
        {
            if (Mathf.Approximately(transform.rotation.eulerAngles.z, 90.0f))
                return 3;
            if (Mathf.Approximately(transform.rotation.eulerAngles.z, 180.0f))
                return 6;
            if (Mathf.Approximately(transform.rotation.eulerAngles.z, 270.0f))
                return 9;
            else
                return 12;

        }
    }



    private void Awake()
    {
        E_txt = GameObject.Find("E_Text");
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        state = State.Idle;
    }

    private void Start()
    {      
        renderer = GetComponent<Renderer>();
        outline = renderer.materials[1];    
    }

    private void Update()
    {
        StateUpdate();
    }
    void StateUpdate()
    {
       switch(state)    
        {
            case State.Idle: 
                {
                    if (isPlayerSee)
                        if (Vector3.Distance(playerTr.position, transform.position) < Distance)
                            ChangeState(State.OnPlayer);                                                      
                } break;
            case State.OnPlayer: 
                {
                    E_txt.transform.position = Camera.main.WorldToScreenPoint(transform.position);
                    if (Input.GetKeyDown(KeyCode.E))                   
                        ChangeState(State.Rot);

                    if (Vector3.Distance(playerTr.position, transform.position) >= Distance)                                      
                        ChangeState(State.Idle);
                
                } break;
            case State.Rot:
                {
                    rotTime += Time.deltaTime;
                    transform.rotation = Quaternion.Slerp(origin, next, rotTime);
                    if (rotTime >= 1.0f)
                    {
                        transform.rotation = next;
                        Debug.Log(num);
                        ChangeState(State.Idle);
                    }

                } break;
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
                } break;
            case State.OnPlayer: 
                {
                    outline.SetColor("_OutlineColor", Color.yellow);
                    E_txt.SetActive(true);
                    
                } break;
            case State.Rot:
                {
                    E_txt.SetActive(false);
                    origin = transform.rotation;
                    next = origin * Quaternion.Euler(0, 0, 90.0f);
                    rotTime = 0.0f;
                } break;
        }

       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {            
        isPlayerSee = true;    
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPlayerSee = false;

        if (state != State.Rot)
            ChangeState(State.Idle);
    }


}
