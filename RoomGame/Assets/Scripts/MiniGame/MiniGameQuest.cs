using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiniGameHelper;

public class MiniGameQuest : MonoBehaviour
{
    public GameObject MiniGameObj;
    MiniGame miniGame;
    [SerializeField] Text infoTxt;

    [SerializeField] Fog fog;

    bool inPlayer = false;
    bool questClear = false;

    private void Start()
    {
        infoTxt = GameObject.Find("InfoText").GetComponent<Text>();
        miniGame = MiniGameObj.GetComponent<MiniGame>();
        miniGame.QuestSetFunc(QuestClear, QuestCloes);
    }

    private void Update()
    {
        if(inPlayer && Input.GetKeyDown(KeyCode.Q))
        {
            infoTxt.gameObject.SetActive(false);
            GameManager.Inst.EnemyMove(false);
            miniGame.MiniGameStart();
        }
          
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !questClear)
        {
            inPlayer = true;
            infoTxt.gameObject.SetActive(true);
            infoTxt.text = "Q를 눌러 조작을 하세요";
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            infoTxt.gameObject.SetActive(false);
            inPlayer = false;
        }
    }

    void QuestClear()
    {
        questClear = true;
        fog.OffFogs();
        GameManager.Inst.EnemyMove(true);
    }

    void QuestCloes()
    {
        GameManager.Inst.EnemyMove(true);
    }

}
