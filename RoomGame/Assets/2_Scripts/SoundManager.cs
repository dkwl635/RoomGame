using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public enum eSFX
{
    NONE,
    GET,

}

public class SoundManager : MonoBehaviour
{
    static SoundManager inst;
    public static SoundManager Inst
    {
        get
        {
            if (inst == null)
            {
                inst = GameObject.FindObjectOfType<SoundManager>();
                if(inst == null)
                {
                    GameObject gameObject = Instantiate(Resources.Load("UI/SoundCanvas") as GameObject);
                    inst = gameObject.GetComponent<SoundManager>();
                }    
                return inst;
            }
            else
                return inst;
        }
    }

    public AudioMixer audioMixer;
    public GameObject Box;
    public Slider SFX_slider;
    public Slider BGM_slider;
    public Button CloseBtn;

    public AudioSource SFX_audiSource;
    public AudioClip[] SFX_AudioClips;

    private void Start()
    {
        CloseBtn.onClick.AddListener(BoxOff);
        SFX_slider.onValueChanged.AddListener(ChangeSFXVolume);
        BGM_slider.onValueChanged.AddListener(ChangeBGMVolume);

        float valeu = 0.0f;
        audioMixer.GetFloat("SFX",out valeu);
        SFX_slider.value = valeu;

        audioMixer.GetFloat("BGM", out valeu);
        BGM_slider.value = valeu;

    }

    public void BoxOn()
    {
        Box.SetActive(true);
    }

    public void BoxOff()
    {
        Box.SetActive(false);
    }

    void ChangeBGMVolume(float value)
    {
        float sound = value;
        if (sound == -40.0f)
            audioMixer.SetFloat("BGM", -80.0f);
        else
            audioMixer.SetFloat("BGM", sound);

    }

    void ChangeSFXVolume(float value)
    {
        float sound = value;
        if (sound == -40.0f)
            audioMixer.SetFloat("SFX", -80.0f);
        else
            audioMixer.SetFloat("SFX", sound);

    }

    public void SoundOnShot(eSFX eSFX)
    {
        SFX_audiSource.PlayOneShot(SFX_AudioClips[(int)eSFX]);
    }
}
