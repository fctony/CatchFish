
using UnityEngine;

public class GoldCollect : MonoBehaviour {

    void OnTriggEnter2D(Collider2D col)
    {
        if (col.tag=="Gold")
        {
            AudioManager.Instance.PlayMus(AudioManager.Instance.goldClip);
            Destroy(col.gameObject);
        }
    }


}
