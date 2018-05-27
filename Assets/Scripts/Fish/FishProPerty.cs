
using UnityEngine;

public class FishProPerty : MonoBehaviour {
    //鱼的最大速度
    public float maxSpeed;
    //鱼的最大数量
    public int maxNum;

    //鱼的生命值
    public int hp;

    //定义鱼的经验
    public int exp;

    //定义鱼的金币
    public int gold;

    //定义鱼死亡的效果
    public GameObject diePrefab;

    //鱼死了调用金币效果
    public GameObject goldPrefab;

    //碰撞检测，鱼碰到边界销毁
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag=="Border")
        {
            Destroy(gameObject);
        }
    }
    //监听函数
    void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp<=0)
        {
            
            GunControl.Instance.gold += gold;
            GunControl.Instance.exp += exp;
            //生成鱼死亡的特效
            GameObject die = Instantiate(diePrefab);
            die.transform.SetParent(gameObject.transform.parent,false);
            die.transform.position = transform.position;
            die.transform.rotation = transform.rotation;
            //生成捕捉到鱼的金币特效
            GameObject goldGo = Instantiate(goldPrefab);
            goldGo.transform.SetParent(gameObject.transform.parent, false);
            goldGo.transform.position = transform.position;
            goldGo.transform.rotation = transform.rotation;
            //如果查找到有特效预制，则播放此特效
            if (gameObject.GetComponent<PlayEffect>()!=null)
            {
                AudioManager.Instance.PlayMus(AudioManager.Instance.rewardClip);
                gameObject.GetComponent<PlayEffect>().PlayEffects();
            }
            Destroy(gameObject);
        }
    }
}
