using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Door door;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            door.IsOpen = true;
            Destroy(this.gameObject);
        }
    }
}
