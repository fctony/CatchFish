using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageMsr : MonoBehaviour {

    //单例
    private static LanguageMsr instance = null;
    public static LanguageMsr Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
        LoadLanguage();
    }

    /// <summary>
    /// 语言
    /// </summary>
    [SerializeField]
    private SystemLanguage language;
    /// <summary>
    /// 相同的key对应不同国家的value
    /// </summary>
    private Dictionary<string, string> dict = new Dictionary<string, string>();

    /// <summary>
    /// 加载预翻译的语言
    /// </summary>
    private void LoadLanguage()
    {
        TextAsset ta = Resources.Load<TextAsset>(language.ToString());
        if (ta == null)
        {
            Debug.LogWarning("没有这个国家的语言翻译文件");
            return;
        }
        //获取每一行的数据
        string[] lines = ta.text.Split('\n');
        //获取key和value
        for (int i = 0; i < lines.Length; i++)
        {
            //检测
            if (string.IsNullOrEmpty(lines[i]))
                continue;
            //通过冒号分割每一行的key和value
            string[] kv = lines[i].Split(':');
            //把key和value添加到字典里面
            dict.Add(kv[0], kv[1]);
            Debug.LogFormat("key:{0},value:{1}", kv[0], kv[1]);
        }
    }


    /// <summary>
    /// 获取对应的value
    /// </summary>
    /// <param name="key"></param>
    /// <returns>如果没有key，就返回空字符串</returns>
    public string GetText(string key)
    {
        if (dict.ContainsKey(key))
        {
            return dict[key];
        }
        else
        {
            return string.Empty;
        }
    }

}
