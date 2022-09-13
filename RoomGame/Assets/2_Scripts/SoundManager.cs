using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

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
                    GameObject gameObject = Instantiate(Resources.Load("UI/SoundManager") as GameObject);
                    inst = gameObject.GetComponent<SoundManager>();
                }

                DontDestroyOnLoad(inst.gameObject);
                return inst;
            }
            else
                return inst;
        }
    }

    public AudioMixer audioMixer;
    public AudioSource audioSource;
    public GameObject Box;
    public Slider SFX_slider;
    public Slider BGM_slider;
    public Button CloseBtn;
    public Button ExitBtn;
    public Button LobbyBtn;


    private void Start()
    { 
        CloseBtn.onClick.AddListener(BoxOff);
        SFX_slider.onValueChanged.AddListener(ChangeSFXVolume);
        BGM_slider.onValueChanged.AddListener(ChangeBGMVolume);

        ExitBtn.onClick.AddListener(OnClick_Exit);
        LobbyBtn.onClick.AddListener(OnClick_Lobby);

        float valeu = 0.0f;
        audioMixer.GetFloat("SFX",out valeu);
        SFX_slider.value = valeu;

        audioMixer.GetFloat("BGM", out valeu);
        BGM_slider.value = valeu;

       
    }

    public void BoxOn()
    {
        Box.SetActive(true);

        if (SceneManager.GetActiveScene().name.Equals("LobbyScene"))
            LobbyBtn.gameObject.SetActive(false);
        else
            LobbyBtn.gameObject.SetActive(true);
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

   public void OnClickBtn()
    {
        audioSource.Play();
    }

    void OnClick_Exit()
    {
        Application.Quit();
    }

    void OnClick_Lobby()
    {
        BoxOff();
        LoadingManger.inst.LoadScene(0);
    }

}
