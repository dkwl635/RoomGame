using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;

    PlayerMovement player;
    WaypointPatrol[] waypointPatrols;
   

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(Inst);
        }
        else
        {
            Destroy(this.gameObject);
        }
           
    }

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        waypointPatrols = GameObject.FindObjectsOfType<WaypointPatrol>();
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
