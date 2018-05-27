
using UnityEngine;

public class Ef_DestroyDelf : MonoBehaviour {

    //延迟1秒后消除死亡动画
    public float delay = 1f;
	void Start () {
        Destroy(gameObject,delay);
	}
	
}
