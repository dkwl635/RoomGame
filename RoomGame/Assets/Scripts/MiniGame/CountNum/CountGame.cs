using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MiniGameHelper;

public class CountGame : MonoBehaviour , MiniGame
{
    int curNum = 0;

    [SerializeField] private GameObject gamePanel;
    [SerializeField] private NumPad[] NumPads;
    List<int> nums = new List<int>();

    bool gameIng = false;
    private void Awake()
    {
        NumPad.Push = true;
    }

    private void Start()
    {
        for (int i = 0; i < NumPads.Length; i++)       
            NumPads[i].NumPadPush = PushNumPad;
        
    }

    public void MiniGameStart()
    {
        if (gameIng)
            return;

        gameIng = true;
        SettingNum();
        this.gameObject.SetActive(true);
    }

    public bool PushNumPad(int num)
    {
      
        int pushNum = num - 1;
        if(curNum == pushNum)//성공
        {
            curNum++;
            return true;
        }
        else //실패
        {
            NumPad.Push = false;
            StartCoroutine(GameFailed());
            return false;
        }
    }

    public void SettingNum()
    {
        nums.Clear();
        for (int i = 1; i <= 9; i++)
            nums.Add(i);    

        for (int i = 0; i < NumPads.Length; i++)
        {
            int rand = Random.Range(0, nums.Count);
            NumPads[i].SetNum(nums[rand]);
            nums.RemoveAt(rand);
        }

        curNum = 0;
        NumPad.Push = true;
    }

    IEnumerator GameFailed()
    {
        for (int i = 0; i < NumPads.Length; i++)
            NumPads[i].SetColor(Color.red);

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < NumPads.Length; i++)
            NumPads[i].SetColor(Color.white);

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < NumPads.Length; i++)
            NumPads[i].SetColor(Color.red);

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < NumPads.Length; i++)
            NumPads[i].SetColor(Color.white);
    }

 
}

