using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Slider BGM_Slider;

    private void Start()
    {
        BGM_Slider.onValueChanged.AddListener(ChangeVolume);
    }

    void ChangeVolume(float value)
    {
        float sound = value;
        if (sound == -40.0f)
            audioMixer.SetFloat("BGM", -80.0f);
        else
            audioMixer.SetFloat("BGM", sound);


    }

}
