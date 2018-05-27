
using UnityEngine;

public class WebProperty : MonoBehaviour {

    //渔网消除的时间
    public float disapperTime;

    //渔网的伤害值
    public int damage;

    void Start()
    {
        Destroy(gameObject,disapperTime);
    }

    //渔网打到鱼之后发送收到上海的消息给鱼
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag=="Fish")
        {
            col.SendMessage("TakeDamage",damage);
        }
    }

}
