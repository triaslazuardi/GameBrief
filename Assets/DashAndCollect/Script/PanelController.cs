using Nitzz.Utility;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public LocalLeaderboard _scrLeaderboard;
    public SettingManage _scrSetting;

    [SerializeField] private GameObject bg;

    [SerializeField] private GameObject panelPlay;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;

    private void Start()
    {
        PlayerPrefs.GetFloat("SFXVolume", 1f);
        PlayerPrefs.GetFloat("MusicVolume", 1f);
    }


    public void OperatePanelPlay(bool isactive) { 
        bg.SetActive(isactive);
        panelPlay.SetActive(isactive);
    }

    public void OperatePanelWin(bool isactive)
    {
        CloseAllPanel();
        bg.SetActive(isactive);
        panelWin.SetActive(isactive);
    }

    public void OperatePanelLose(bool isactive)
    {
        CloseAllPanel();
        bg.SetActive(isactive);
        panelLose.SetActive(isactive);
    }

    public void OpenPanelLeaderboard(bool isActive) {
        bg.SetActive(isActive);

        if (isActive)
        {
            _scrLeaderboard.OpenPanel();
        }
        else {
            _scrLeaderboard.ClosePanel();
        } 
    }

    public void OperateSetting(bool isactive) {
        bg.SetActive(isactive);
        _scrSetting.OpenPanel(isactive);
    }

    public void CloseAllPanel() {
        _scrLeaderboard.ClosePanel();
        _scrSetting.OpenPanel(false);
    }

    public void SoundClick() {
        AudioManager.instance.PlaySFX("click");
    }
}


