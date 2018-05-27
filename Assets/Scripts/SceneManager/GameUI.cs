using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public GameObject settingPanel;

    public Toggle muteToggle;

    void Start()
    {
        muteToggle.isOn = !AudioManager.Instance.IsMute;
    }

    //转换音乐状态
    public void SwitchMute(bool isOn)
    {
        AudioManager.Instance.SwitchMuteState(isOn);
    }

    //转回菜单
    public void BackTomenu()
    {
        PlayerPrefs.SetInt("gold",GunControl.Instance.gold);
        PlayerPrefs.SetInt("lv", GunControl.Instance.Lv);
        PlayerPrefs.SetInt("exp", GunControl.Instance.exp);
        PlayerPrefs.SetFloat("bcd", GunControl.Instance.bigTimer);
        PlayerPrefs.SetFloat("scd", GunControl.Instance.smallTimer);
        int temp = (AudioManager.Instance.IsMute == false) ? 0 : 1;
        PlayerPrefs.SetInt("mute",temp );
        SceneManager.LoadScene("Menu");
    }

    //设置菜单
    public void Setting()
    {
        settingPanel.SetActive(true);
    }

    //关闭界面
    public void Close()
    {
        settingPanel.SetActive(false);
    }

}
