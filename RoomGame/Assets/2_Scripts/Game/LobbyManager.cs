using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LobbyManager : MonoBehaviour
{
    public Button gameStartBtn;

    private void Start()
    {
        gameStartBtn.onClick.AddListener(GameStart);
    }

    void GameStart()
    {
        LoadingManger.inst.LoadScene(1);
    }
    
}
