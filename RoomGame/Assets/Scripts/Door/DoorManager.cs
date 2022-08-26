using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorManager : MonoBehaviour
{
    class KeyBox
    {
        bool isUse = false;
        public GameObject obj;
        public Image keyImg;

        public bool IsUse { get { return isUse; } }
        
        public void SetKeyBox(GameObject gameObject)
        {
            obj = gameObject;
            keyImg = gameObject.GetComponentInChildren<Image>();
        }

        public void OnKeyBox(Key key)
        {
            isUse = true;
            obj.SetActive(true);
            keyImg.color = key.keyColor;
        }

       public void OffKeyBox()
        {
            isUse = false;
            obj.SetActive(false);
        }
       
    }

    public static DoorManager Inst;
    [SerializeField] GameObject[] KeyBoxObjs;
    [SerializeField] List<Key> keys = new List<Key>();
    [SerializeField] List<KeyBox> keyBoxes = new List<KeyBox>();
    [SerializeField] Dictionary<int, KeyBox> keyValuePairs = new Dictionary<int, KeyBox>();



    private void Awake()
    {
        Inst = this;
    }


    private void Start()
    {
        for (int i = 0; i < KeyBoxObjs.Length; i++)
        {
            KeyBox box = new KeyBox();
            box.SetKeyBox(KeyBoxObjs[i]);
            keyBoxes.Add(box);
        }
    }



    public void AddKey(Key key)
    {
        for (int i = 0; i < keyBoxes.Count; i++)
        {
            if(keyBoxes[i].IsUse == false)
            {
                keyBoxes[i].OnKeyBox(key);
                keyValuePairs.Add(key.keyNum, keyBoxes[i]);
                break;
            }
        }

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
                keyValuePairs[doorNum].OffKeyBox();
                keyValuePairs.Remove(doorNum);
                keys.RemoveAt(i);
                break;
            }
        }

        door.CheckDoor(getKey);
    }


}
