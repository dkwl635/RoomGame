using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{  
    bool isPlayerInRange;
    Transform Tr_Player;
    public GameEnding gameEnding;

    private void Update()
    {     
        //if(isPlayerInRange)
        //{
        //    Vector3 direction = Tr_Player.position - transform.position + Vector3.up;
        //    Ray ray = new Ray(transform.position, direction);
        //    RaycastHit raycastHit;
        //    if (Physics.Raycast(ray, out raycastHit))
        //    {
        //     if(raycastHit.collider.CompareTag("Player"))
        //        {
        //            gameEnding.CaughtPlayer();
        //        }
               
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameEnding.CaughtPlayer();
            //isPlayerInRange = true;
            //Tr_Player = other.transform;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        isPlayerInRange = false;
    //    }
    //}
   

}
