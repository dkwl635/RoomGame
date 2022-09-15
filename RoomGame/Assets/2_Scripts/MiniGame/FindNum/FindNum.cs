using MiniGameHelper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindNum : MonoBehaviour, MiniGame
{
    public GameObject gamePanel;
    [Header("ColorNum")]
    [SerializeField] Text[] answerNumText;
    [SerializeField] Button[] numUpDownBtn;
    [SerializeField] Image[] numImage;
    [SerializeField] Button OkBtn;
    [SerializeField] Button closeBtn;
    int[] answerNums = new int[3] { 0, 0, 0 };

    QuestEvent ClearFunc;
    QuestEvent CloesFunc;

    [SerializeField] bool isPush = false;


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

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
        isPush = true;
        gamePanel.SetActive(true);
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
        if (!isPush)
            return;

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
        gamePanel.SetActive(false);
    }

    void OkBtnFunc()
    {
        if(answerNums[0] == 7 && answerNums[1] == 5 && answerNums[2] == 2)//성공
        {
            ClearQuest();
        }
        else //실패
        {
            StartCoroutine(FailedQuest());
        }
    }

    void ClearQuest()
    {
        audioSource.PlayOneShot(audioClips[1]);
        ClearFunc?.Invoke();//외부 연결된 함수 실행
        gamePanel.SetActive(false);

    }

    IEnumerator FailedQuest()
    {
        isPush = false;
        audioSource.PlayOneShot(audioClips[0]);
        for (int i = 0; i < numImage.Length; i++)
            numImage[i].color =Color.red;

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < numImage.Length; i++)
            numImage[i].color = Color.white;

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < numImage.Length; i++)
             numImage[i].color = Color.red;

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < numImage.Length; i++)
            numImage[i].color = Color.white;

        isPush = true;
        yield return null;
    }

}   
