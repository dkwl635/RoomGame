using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;

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
        waypointPatrols = GameObject.FindObjectsOfType<WaypointPatrol>();
    }


    public void EnemyMove(bool IsMove)//움직이는 모든 유령 정지/이동
    {
        for (int i = 0; i < waypointPatrols.Length; i++)
            waypointPatrols[i].MoveOnOff(IsMove);
    }
}
