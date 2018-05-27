using System.Collections;
using UnityEngine;

public class HideSelf : MonoBehaviour {

    public IEnumerator HideSelfs(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }


}
