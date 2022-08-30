using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{

    public NavMeshAgent navMeshAgent;
    public Transform[] Tr_Waypoints;

    int currentWaypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.SetDestination(Tr_Waypoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            //다음 위치 계산
            currentWaypointIndex = (currentWaypointIndex + 1) % Tr_Waypoints.Length;
            navMeshAgent.SetDestination(Tr_Waypoints[currentWaypointIndex].position);
        }
    }

    public void MoveOnOff(bool isMove)
    {
        if(isMove)
        {
            navMeshAgent.isStopped = false;
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
    }
}
