using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MiniGameHelper;

public class CountGame : MonoBehaviour, MiniGame
{
    int curNum = 0;

    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject numPadsObj;
    [SerializeField] private GameObject[] infoBox;
    [SerializeField] private NumPad[] NumPads;
    List<int> nums = new List<int>();

    RectTransform numPadsRect;
    bool gameIng = false;
    QuestEvent ClearFunc;

    private void Awake()
    {
        numPadsRect = numPadsObj.GetComponent<RectTransform>();

        NumPad.Push = true;
    }

    private void Start()
    {
        for (int i = 0; i < NumPads.Length; i++)
            NumPads[i].NumPadPush = PushNumPad;
    }

    public void QuestSetFunc(QuestEvent ClearFunc, QuestEvent CloesFunc)
    {
        this.ClearFunc = ClearFunc;
    }

    public void MiniGameStart()
    {
        if (gameIng)
            return;

        gameIng = true;
        SettingNum();
        this.gameObject.SetActive(true);

        StartCoroutine(OpenUI());
    }

    IEnumerator OpenUI()
    {
        float timer = 0.0f;
        Vector2 originPos = numPadsRect.anchoredPosition;
        Vector2 endPos = Vector2.zero;
        Vector2 pos = Vector2.zero;

        while (timer < 1.0f)
        {
            timer += Time.deltaTime * 2.0f;
            yield return null;

            pos = Vector2.Lerp(originPos, endPos, timer);
            numPadsRect.anchoredPosition = pos;

        }

        infoBox[0].SetActive(true);
        infoBox[1].SetActive(true);
    }



    public bool PushNumPad(int num)
    {

        int pushNum = num - 1;
        if (curNum == pushNum)//성공
        {
            curNum++;

            if (curNum > 8)
                MiniGameClear();

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

        SettingNum();
    }

    public void MiniGameClear()
    {
        ClearFunc?.Invoke();//외부 연결된 함수 실행
        gamePanel.SetActive(false);
    }


}