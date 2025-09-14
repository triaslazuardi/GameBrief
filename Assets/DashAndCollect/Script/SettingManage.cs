using Nitzz.Utility;
using UnityEngine;
using UnityEngine.UI;

public class SettingManage : MonoBehaviour
{
    public GameObject panel;
    public Slider sliderSFX;
    public Slider sliderMusic;

    private void Start()
    {
        sliderSFX.onValueChanged.AddListener(OnSFXChanged);
        sliderMusic.onValueChanged.AddListener(OnMusicChanged);

        sliderSFX.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

    }

    public void OnSFXChanged(float value)
    {
        AudioManager.instance.SFXVolume(value);
        PlayerPrefs.SetFloat("VolumeSFX", value);
    }

    public void OnMusicChanged(float value)
    {
        AudioManager.instance.MusicVolume(value);
        PlayerPrefs.SetFloat("VolumeMusic", value);
    }


    public void OpenPanel(bool isactive)
    {
        panel.SetActive(isactive);
    }
}
