using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UItext : MonoBehaviour {

    [SerializeField]
    private string key;

    void Start()
    {
        if (!string.IsNullOrEmpty(key))
        {
            string value = LanguageMsr.Instance.GetText(key);
            if (!string.IsNullOrEmpty(value))
            {
                GetComponent<Text>().text = value;
            }
        }

    }
}
