using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Key"))
         {
            DoorManager.Inst.AddKey(other.GetComponent<Key>());
            other.gameObject.SetActive(false);
        }
        else if(other.name.Contains("Door"))
        {
            DoorManager.Inst.OpenDoor(other.GetComponent<Door>());
        }

    }

}
