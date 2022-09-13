using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManger : MonoBehaviour
{
    public static LoadingManger inst;
    public Image fadeImg;

    private void Awake()
    {
        if(inst)
            Destroy(this.gameObject);
        else
        {
            inst = this;
            DontDestroyOnLoad(inst.gameObject);
        }
    }

    public void LoadScene(int sceneIdx)
    {
        StartCoroutine(LoadScene_Co(sceneIdx));
    }

    IEnumerator LoadScene_Co(int sceneIdx)
    {
        float a = fadeImg.color.a;
        Color color = fadeImg.color;
        fadeImg.gameObject.SetActive(true);
        while (a < 1.0f)
        {
            a += Time.deltaTime;
            color.a = a;
            fadeImg.color = color;
            yield return null;
        }

        //SceneManager.LoadScene(sceneIdx);
        var _var = SceneManager.LoadSceneAsync(sceneIdx);
        while (!_var.isDone)
            yield return null;

        while (a > 0)
        {
            a -= Time.deltaTime;
            color.a = a;
            fadeImg.color = color;
            yield return null;
        }
        fadeImg.gameObject.SetActive(false);
    }

}
