
using UnityEngine;

public class PlayEffect : MonoBehaviour {

    //特效
    public GameObject[] effectPrefabs;

    public void PlayEffects()
    {
        foreach (GameObject effectPre in effectPrefabs)
        {
            Instantiate(effectPre);
        }
    }



}
