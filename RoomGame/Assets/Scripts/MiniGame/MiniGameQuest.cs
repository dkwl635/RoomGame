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

    bool inplayer = false;

    private void Start()
    {
        infoTxt = GameObject.Find("InfoText").GetComponent<Text>();
        miniGame = MiniGameObj.GetComponent<MiniGame>();
    }

    private void Update()
    {
        if(inplayer && Input.GetKeyDown(KeyCode.Q))
        {
            miniGame.MiniGameStart();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inplayer = true;
        infoTxt.gameObject.SetActive(true);
        infoTxt.text = "Q를 눌러 조작을 하세요";
    }

    private void OnTriggerExit(Collider other)
    {

        infoTxt.gameObject.SetActive(false);
        inplayer = false;
    }

}
