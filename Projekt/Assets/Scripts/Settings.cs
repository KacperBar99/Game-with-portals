using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using Unity.VisualScripting;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Toggle fullscreenToggle;

    private Resolution[] resolutions;

    

    // Start is called before the first frame update
    void Start()
    {
        this.resolutions = Screen.resolutions;
        this.resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " " + (int)resolutions[i].refreshRateRatio.value + "Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        this.resolutionDropdown.AddOptions(options);
        this.resolutionDropdown.value = currentResolutionIndex;
        this.resolutionDropdown.RefreshShownValue();
        this.loadSettings();
        this.applySettings();
    }

    public void save_settings()
    {
        using (StreamWriter writer = new StreamWriter(@"settings.settings"))
        {
            writer.WriteLine(this.resolutionDropdown.value);
            writer.WriteLine(this.volumeSlider.value);
            writer.WriteLine(fullscreenToggle.isOn);
        }
    }

    public void setVolume(float volume)
    {
        this.audioMixer.SetFloat("Volume", volume);
    }

    public void setResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRateRatio);
        Application.targetFrameRate = (int)resolution.refreshRateRatio.value;
    }

    public void setFullscreen(bool isFullScreen)
    {
        if (isFullScreen) 
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        
    }

    public void loadSettings()
    {
        string line1;
        string line2;
        string line3;
        using (StreamReader reader = new StreamReader(@"settings.settings"))
        {
            line1 = reader.ReadLine();
            line2 = reader.ReadLine();
            line3 = reader.ReadLine();
        }
        if (line1 != "-1")
            this.resolutionDropdown.value = Convert.ToInt32(line1);
        this.volumeSlider.value = Convert.ToSingle(line2);
        fullscreenToggle.isOn = Convert.ToBoolean(line3);
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
    }
    public void applySettings()
    {
        this.setFullscreen(this.fullscreenToggle.isOn);
        this.setResolution(this.resolutionDropdown.value);
        this.setVolume(this.volumeSlider.value);

    }
}
