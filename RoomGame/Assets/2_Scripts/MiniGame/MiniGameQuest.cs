using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiniGameHelper;

public class MiniGameQuest : MonoBehaviour
{
    MiniGame miniGame;
    [SerializeField] Text infoTxt;
    [SerializeField] Fog fog;

    Outline meshOutline;

    bool inPlayer = false;
    bool questClear = false;

    private void Awake()
    {
        meshOutline = GetComponentInChildren<Outline>();

    }
    private void Start()
    {
        miniGame = GetComponent<MiniGame>();
        miniGame.QuestSetFunc(QuestClear, QuestCloes);
    }

    private void Update()
    {
        if(inPlayer && Input.GetKeyDown(KeyCode.Q))
        {
            infoTxt.gameObject.SetActive(false);         
            miniGame.MiniGameStart();
            SFXManager.Inst.SoundOnShot(eSFX.OPENQUEST);
            GameManager.Inst.EnemyMove(false);
            GameManager.Inst.PlayerMove(false);
        }
          
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !questClear)
        {
            inPlayer = true;
            infoTxt.gameObject.SetActive(true);
            infoTxt.text = "Q를 눌러 조작을 하세요";
            meshOutline.SetColor(Color.yellow);
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            meshOutline.SetColor(Color.clear);
            infoTxt.gameObject.SetActive(false);
            inPlayer = false;
        }
    }

    void QuestClear()
    {
        questClear = true;
        fog.OffFogs();
        meshOutline.SetColor(Color.clear);
        GameManager.Inst.EnemyMove(true);
        GameManager.Inst.PlayerMove(true);
    }

    void QuestCloes()
    {
        GameManager.Inst.EnemyMove(true);
        GameManager.Inst.PlayerMove(true);
    }

}
