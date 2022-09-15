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

    PicturePuzzle picturePuzzle;
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

    AudioSource audioSource;

    public int num //각도에 따른 방향 값 리턴
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


    private void Awake() //필요한 오브젝트 캐싱
    {
        picturePuzzle = GameObject.FindObjectOfType<PicturePuzzle>();
        E_txt = GameObject.Find("E_Text");
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        state = State.Idle;
        renderer = GetComponent<Renderer>();
        outline = renderer.materials[1];
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        StateUpdate();
    }

    void StateUpdate() //상태에 따른 업데이트
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
                        ChangeState(State.Idle);
                    }

                } break;
        }
    }

    void ChangeState(State newState) //상태가 변화 됬을떄
    {
        if (state == newState)
            return;

        state = newState;
        switch (state)
        {
            case State.Idle: 
                {
                    outline.SetColor("_OutlineColor", Color.clear);
                    picturePuzzle.CheckPuzzle();
                    E_txt.SetActive(false);
                } break;
            case State.OnPlayer: 
                {
                    outline.SetColor("_OutlineColor", Color.yellow);
                    E_txt.SetActive(true);
                    
                } break;
            case State.Rot:
                {
                    audioSource.Play();
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
