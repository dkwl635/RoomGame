using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    public float displayImageDuration = 1.0f;

    bool isPlayerAtExit;
    bool isPlayerCaught;

    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
 
    float timer;
    bool isHasAudioPlayer;

    public AudioSource AS_Exit;
    public AudioSource AS_Caught;

    bool end = false;

    public GameObject endBtns;

    private void Start()
    {
        timer = 0.0f;
    }

    private void Update()
    {
        if(isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup,false, AS_Exit);
        }
        else if (isPlayerCaught)
        {          
            EndLevel(caughtBackgroundImageCanvasGroup,true,AS_Caught);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("JohnLemon"))
        {
            isPlayerAtExit = true;
        }

    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (end)
            return;

        timer += Time.deltaTime;
        imageCanvasGroup.alpha = timer / fadeDuration;

        if(!isHasAudioPlayer)
        {
            audioSource.Play();
            isHasAudioPlayer = true;
        }

        

        if (timer > fadeDuration + displayImageDuration)
        {
            end = true;

            if (doRestart)
            {
                LoadingManger.inst.LoadScene(1);
            }
            else
            {
                endBtns.SetActive(true);
            }
        }
    }

    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }

    public void OnClick_ToLobby()
    {
        LoadingManger.inst.LoadScene(0);
    }

    public void OnClick_GameExit()
    {
        Application.Quit();
    }

}
