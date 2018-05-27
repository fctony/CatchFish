
using UnityEngine;

public class Ef_WaterWave : MonoBehaviour {

    //主场景水波纹图集
    public Texture[] texture;

    private Material material;

    private int index = 0;


    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        InvokeRepeating("ChangeTex", 0, 0.2f);

    }


    void ChangeTex()
    {
        material.mainTexture = texture[index];
        index = (index + 1) % texture.Length;
    }
}
