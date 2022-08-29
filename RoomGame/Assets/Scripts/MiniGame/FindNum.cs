using MiniGameHelper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindNum : MonoBehaviour, MiniGame
{
    [Header("ColorNum")]
    [SerializeField] Text[] answerNumText;
    [SerializeField] Button[] numUpDownBtn;

    [SerializeField] Button OkBtn;
    [SerializeField] Button closeBtn;
    int[] answerNums = new int[3] { 0, 0, 0 };

    QuestEvent ClearFunc;
    QuestEvent CloesFunc;


    public void QuestSetFunc(QuestEvent ClearFunc, QuestEvent CloesFunc)
    {
        this.ClearFunc = ClearFunc;
        this.CloesFunc = CloesFunc;
    }
    public void MiniGameClear()
    {
        ClearFunc?.Invoke();
    }

    public void MiniGameStart()
    {          
        this.gameObject.SetActive(true);
    }


    private void Start()
    {
        //1
        numUpDownBtn[0].onClick.AddListener(() => UpDownBtn(0, true));
        numUpDownBtn[1].onClick.AddListener(() => UpDownBtn(0, false));
        //2
        numUpDownBtn[2].onClick.AddListener(() => UpDownBtn(1, true));
        numUpDownBtn[3].onClick.AddListener(() => UpDownBtn(1, false));
        //3
        numUpDownBtn[4].onClick.AddListener(() => UpDownBtn(2, true));
        numUpDownBtn[5].onClick.AddListener(() => UpDownBtn(2, false));

        OkBtn.onClick.AddListener(OkBtnFunc);
        closeBtn.onClick.AddListener(CloesUI);
    }

    void UpDownBtn(int idx, bool Up)
    {
        if (Up)
        {
            answerNums[idx]++;
            if (answerNums[idx] > 9)
                answerNums[idx] = 0;
        }
        else
        {
            answerNums[idx]--;
            if (answerNums[idx] < 0)
                answerNums[idx] = 9;
        }

        answerNumText[idx].text= answerNums[idx].ToString();
    }

    void CloesUI()
    {
        CloesFunc?.Invoke();
        this.gameObject.SetActive(false);
    }

    void OkBtnFunc()
    {
        if(answerNums[0] == 7 && answerNums[1] == 5 && answerNums[2] == 2)
        {
            ClearQuest();
        }
    }

    void ClearQuest()
    {
        ClearFunc?.Invoke();//외부 연결된 함수 실행
        this.gameObject.SetActive(false);
    }
}   
