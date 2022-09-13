using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Inst;

    public PlayerMovement player;
    WaypointPatrol[] waypointPatrols;
    public Vector3 PlayerPos { get { return player.transform.position; } }

    [Header("ItemData")]
    [SerializeField] private ItemData[] itemDatas;
    public Dictionary<int, ItemData> ItemDates = new Dictionary<int, ItemData>();

    [SerializeField] Button congifBoxBtn;

    private void Awake()
    {
        Inst = this;
      
        for (int i = 0; i < itemDatas.Length; i++)
        {
            if (ItemDates.ContainsKey(itemDatas[i].item_Id))
                return;

            ItemDates.Add(itemDatas[i].item_Id, itemDatas[i]);
        } 

      
        
    }


    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        waypointPatrols = GameObject.FindObjectsOfType<WaypointPatrol>();

        congifBoxBtn.onClick.AddListener(OnCongifBox);
    }

    void OnCongifBox()
    {
        SoundManager.Inst.BoxOn();
    }

    public void EnemyMove(bool IsMove)//움직이는 모든 유령 정지/이동
    {
        for (int i = 0; i < waypointPatrols.Length; i++)
            waypointPatrols[i].MoveOnOff(IsMove);
    }

    public void PlayerMove(bool IsMove)
    {
        player.IsMove = IsMove;
    }
}
