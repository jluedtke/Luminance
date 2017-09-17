using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public GameObject creds;
    public GameObject optionsMenu;

    public Toggle[] toggles;
    public Slider[] sliders;
    public int[] resolutionWidth;

    int lastResolutionIndex;

    public Toggle fullScreenToggle;

    private void Start()
    {
        lastResolutionIndex = PlayerPrefs.GetInt("active resolution index");
        bool isFullScreen = (PlayerPrefs.GetInt("isFullscreen") == 1)? true: false;

        //toggles[0].value = AudioManager.instance.MasterVolumePercent;
        //toggles[1].value = AudioManager.instance.MasterVolumePercent;
        //toggles[2].value = AudioManager.instance.MasterVolumePercent;

        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].isOn = i == lastResolutionIndex;
        }

        fullScreenToggle.isOn = isFullScreen;
    }

    public void ShowCredits()
    {
        creds.SetActive(!creds.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }

    public void SetResolution(int i)
    {
        if (toggles[i].isOn)
        {
            lastResolutionIndex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(resolutionWidth[i], (int)(resolutionWidth[i] / aspectRatio), false);
            PlayerPrefs.SetInt("active resoultion index", lastResolutionIndex);
            PlayerPrefs.Save();
        }
    }

    public void SetMaster(float i)
    {
        //AudioManager.instance.SetVolume(i, AudioManager.AudioChannel.Master);
    }

    public void SetMusic(float i)
    {
        //AudioManager.instance.SetVolume(i, AudioManager.AudioChannel.Music);
    }

    public void SetSFX(float i)
    {
        //AudioManager.instance.SetVolume(i, AudioManager.AudioChannel.Sfx);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].interactable = !isFullScreen;
        }

        if (isFullScreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            if (lastResolutionIndex < 0 || lastResolutionIndex > 2)
            {
                lastResolutionIndex = 1;
            }
            SetResolution(lastResolutionIndex);
        }

        PlayerPrefs.SetInt("isFullscreen", ((isFullScreen) ? 1 : 0));
        PlayerPrefs.Save();
    }

}
