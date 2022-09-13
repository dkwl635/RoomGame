using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum eSFX
{
    TEST,
    GET,
    BUTTON,
    DOOR,
    HIT,
    OPENQUEST,
}


public class SFXManager : MonoBehaviour
{
    public static SFXManager Inst;

    public AudioSource SFX_audiSource;
    public AudioClip[] SFX_AudioClips;

    private void Awake()
    {
        if (Inst == null)
            Inst = this;
    }

    private void Start()
    {

        var btns = GameObject.FindObjectsOfType<Button>(true);
        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].onClick.AddListener(() => { SoundOnShot(eSFX.BUTTON); });
        }

    }

    public void SoundOnShot(eSFX eSFX)
    {
        SFX_audiSource.PlayOneShot(SFX_AudioClips[(int)eSFX]);
    }
}
