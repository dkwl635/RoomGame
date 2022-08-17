using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager Inst;

    [SerializeField] List<Key> keys = new List<Key>();


    private void Awake()
    {
        Inst = this;
    }


    private void Start()
    {
      
    }



    public void AddKey(Key key)
    {
        keys.Add(key);
    }

    public void OpenDoor(Door door)
    {
        int doorNum = door.doorNum;
        bool getKey = false;
        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i].keyNum == doorNum)
            {
                getKey = true;
                keys.RemoveAt(i);
                break;
            }
        }

        door.CheckDoor(getKey);
    }


}
