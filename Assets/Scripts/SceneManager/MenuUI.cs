using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuUI : MonoBehaviour {
    public void Start()
    {
        PlayerPrefs.DeleteKey("gold");
        PlayerPrefs.DeleteKey("exp");
        PlayerPrefs.DeleteKey("bcd");
        PlayerPrefs.DeleteKey("scd");
        PlayerPrefs.DeleteKey("lv");
        SceneManager.LoadScene("Game");
    }

    public void Continue()
    {
        SceneManager.LoadScene("Game");
    }

    public void exit()
    {
        Application.Quit();
    }

}
