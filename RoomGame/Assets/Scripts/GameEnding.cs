using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    public float displayImageDuration = 1.0f;

    bool isPlayerAtExit;
    bool isPlayerCaught;

    public CanvasGroup CG_ExitBackgroundImageCanvasGroup;
    public CanvasGroup CG_CaughtBackgroundImageCanvasGroup;

    float timer;
    bool isHasAudioPlayer;

    public AudioSource AS_Exit;
    public AudioSource AS_Caught;


    private void Update()
    {
        if(isPlayerAtExit)
        {
            EndLevel(CG_ExitBackgroundImageCanvasGroup,false, AS_Exit);
        }
        else if (isPlayerCaught)
        {          
            EndLevel(CG_CaughtBackgroundImageCanvasGroup,true,AS_Caught);
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
        timer += Time.deltaTime;
        imageCanvasGroup.alpha = timer / fadeDuration;

     if(!isHasAudioPlayer)
        {
            audioSource.Play();
            isHasAudioPlayer = true;
        }
      
        

        if(timer > fadeDuration + displayImageDuration)
        {
            if(doRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Debug.Log("Goal");             
                Application.Quit();
            }
            timer = 0.0f;

        }

    }

    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }
}
