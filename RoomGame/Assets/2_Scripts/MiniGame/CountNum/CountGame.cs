using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MiniGameHelper;

public class CountGame : MonoBehaviour, MiniGame
{
    int curNum = 0; //���� �������ϴ� ����

    [SerializeField] private GameObject gamePanel; //�� ���� UI�г�
    [SerializeField] private GameObject numPadsObj;//���е� UI�г�
    [SerializeField] private GameObject[] infoBox; //�ȳ� ����
    [SerializeField] private NumPad[] NumPads;  //���� ���е� ������Ʈ
    List<int> nums = new List<int>();          //������ ���ڸ� �ֱ����� �ӽ� ����Ʈ

    RectTransform numPadsRect;
    bool gameIng = false;
    QuestEvent ClearFunc; //Ŭ����� �� �ݹ��Լ�

    [SerializeField] private AudioSource audioSource; //���� ȿ���� ����
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

    public void QuestSetFunc(QuestEvent ClearFunc, QuestEvent CloesFunc) //�ʿ��� �Լ� ����
    {
        this.ClearFunc = ClearFunc;
    }

    public void MiniGameStart() //����
    {
        if (gameIng)
            return;

        gameIng = true;
        SettingNum();   //�� ����
    
        StartCoroutine(OpenUI()); //���� UI Open
    }

    IEnumerator OpenUI() //���½� ��Ÿ�� ȿ��
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


    public bool PushNumPad(int num) //���е忡�� ȣ��� �Լ�
    {
        audioSource.PlayOneShot(audioClips[0]);

        int pushNum = num - 1;
        if (curNum == pushNum)//����
        {
            curNum++;

            if (curNum > 8)
                MiniGameClear();

            return true;
        }
        else //����
        {
            NumPad.Push = false;
            StartCoroutine(GameFailed());
            return false;
        }

    }

    public void MiniGameClear() //������
    {
        audioSource.PlayOneShot(audioClips[2]);
        ClearFunc?.Invoke();//�ܺ� ����� �Լ� ����
        gamePanel.SetActive(false); //UI off
    }


    public void SettingNum() //������ ���� ����
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

    IEnumerator GameFailed() //���н� ȿ��
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