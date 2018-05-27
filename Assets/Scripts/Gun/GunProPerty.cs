using UnityEngine;

public class GunProPerty : MonoBehaviour {

    //定义子弹速度
    public float speed;
    //定义子弹伤害值
    public int damage;

    //定义子弹所属的渔网预制体
    public GameObject WebPrefab;

    //子弹撞到鱼发射出渔网，碰到边界或鱼销毁自身
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag=="Border")
        {
            Destroy(gameObject);
        }

        if (col.tag=="Fish")
        {
            GameObject web = Instantiate(WebPrefab);
            web.transform.SetParent(gameObject.transform.parent,false);
            web.transform.position = gameObject.transform.position;
            web.GetComponent<WebProperty>().damage = damage;
            Destroy(gameObject);
        }
    }
}
