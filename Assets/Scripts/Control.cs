using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Control : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    float currentVolume;
    void Start()
    {
        if (PlayerPrefs.HasKey("VolumePreference"))
            volumeSlider.value =
                        PlayerPrefs.GetFloat("VolumePreference");
        else
            volumeSlider.value =
                        PlayerPrefs.GetFloat("VolumePreference");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game Scene");
    }
    public void SetVolume(int volume)
    {
        audioMixer.SetFloat("Volume", (float) (volume / 10));
        currentVolume = volume;
        PlayerPrefs.SetFloat("VolumePreference",
                   currentVolume);
    }
}
