using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MiniGameHelper;

public class CountGame : MonoBehaviour, MiniGame
{
    int curNum = 0; //현재 눌러야하는 순서

    [SerializeField] private GameObject gamePanel; //본 퍼즐 UI패널
    [SerializeField] private GameObject numPadsObj;//넘패드 UI패널
    [SerializeField] private GameObject[] infoBox; //안내 문구
    [SerializeField] private NumPad[] NumPads;  //각각 넘패드 오브젝트
    List<int> nums = new List<int>();          //무작위 숫자를 넣기위한 임시 리스트

    RectTransform numPadsRect;
    bool gameIng = false;
    QuestEvent ClearFunc; //클리어시 들어갈 콜백함수

    [SerializeField] private AudioSource audioSource; //음향 효과를 위한
    [SerializeField] private AudioClip[] audioClips;

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

    public void QuestSetFunc(QuestEvent ClearFunc, QuestEvent CloesFunc) //필요한 함수 저장
    {
        this.ClearFunc = ClearFunc;
    }

    public void MiniGameStart() //시작
    {
        if (gameIng)
            return;

        gameIng = true;
        SettingNum();   //판 셋팅
    
        StartCoroutine(OpenUI()); //게임 UI Open
    }

    IEnumerator OpenUI() //오픈시 나타낼 효과
    {
        gamePanel.SetActive(true);
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


    public bool PushNumPad(int num) //넘패드에서 호출될 함수
    {
        audioSource.PlayOneShot(audioClips[0]);

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

    public void MiniGameClear() //성공시
    {
        audioSource.PlayOneShot(audioClips[2]);
        ClearFunc?.Invoke();//외부 연결된 함수 실행
        gamePanel.SetActive(false); //UI off
    }


    public void SettingNum() //무작위 숫자 선정
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

    IEnumerator GameFailed() //실패시 효과
    {
        audioSource.PlayOneShot(audioClips[1]);

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



}