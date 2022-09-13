using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LobbyManager : MonoBehaviour
{
    public Button gameStartBtn;
    public Button soundBtn;

    private void Start()
    {
        gameStartBtn.onClick.AddListener(GameStart);
        soundBtn.onClick.AddListener(() => { SoundManager.Inst.BoxOn(); });
    }

    void GameStart()
    {
        LoadingManger.inst.LoadScene(1);
    }
    
}
