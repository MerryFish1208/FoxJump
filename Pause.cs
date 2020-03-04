using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioMixer audioMixer;

    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        GameObject.Find("Canvas/PauseMenu").SetActive(false);
        Time.timeScale = 1f;
    }
    public void  SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }
}
