
using UnityEngine;

public class AudioManager : MonoBehaviour {


    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }
    //game场景里的所有音乐
    public AudioSource bgmMusic;

    public AudioClip seaWaveClip;

    public AudioClip goldClip;

    public AudioClip rewardClip;

    public AudioClip fireClip;

    public AudioClip changeClip;

    public AudioClip lvUpClip;

    //是否静音
    private bool isMute = false;

    public bool IsMute
    {
        get
        {
            return isMute;
        }
    }

    void Awake()
    {
        _instance = this;

        isMute = (PlayerPrefs.GetInt("mute", 0) == 0) ? false : true;

        DoMute();
    }

    public void SwitchMuteState(bool isOn)
    {
        isMute = !isOn;

        DoMute();
    }

    void DoMute()
    {
        if (isMute)
        {
            bgmMusic.Pause();
        }
        else
        {
            bgmMusic.Play();
        }

    }

    public void PlayMus(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip,new Vector3(0,0,-10));
    }


}
