using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class GunControl : MonoBehaviour {

    //单例模式
    private static GunControl _instance;
    public static GunControl Instance
    {
        get
        {
            return _instance;
        }
    }
    //子弹消耗金币的文本框
    public Text oneShotCostText;
    //玩家等级的文本框
    public Text lvText;
    //玩家等级名字的文本框
    public Text lvNameText;
    //金币数的文本框
    public Text goldText;
    //小的倒计时的文本框
    public Text smallTimerCutText;
    //大的倒计时的文本框
    public Text bigTimerCutText;

    //返回按钮
    public Button backButton;
    //设置按钮
    public Button settingButton;
    //经验条
    public Slider expSlider;

    //奖金按钮
    public Button priceButton;

    public Color goldColor;

    //升级标题
    public GameObject OverLv;
    //换枪特效
    public GameObject changeEffect;
    //开火特效
    public GameObject fireEffect;
    //升级特效
    public GameObject lvEffect;
    //金币特效
    public GameObject goldEffect;
    //所有的背景
    public Sprite[] bgSprite;
    //背景图片
    public Image bgImage;
    //背景索引
    public int bgIndex;
    //水花效果
    public GameObject seaWaveEffect;


    /// <summary>
    /// 初始化数据
    /// </summary>
 
    //玩家等级,金币数,经验值，初始时间倒计时，计时器，玩家等级名
    public int Lv=0;
    public int exp = 0;
    public int gold = 500;
    public const int bigCountDown = 240;
    public const int smallCountDown = 60;
    public float bigTimer = bigCountDown;
    public float smallTimer = smallCountDown;



    //获得所有枪的游戏数组
    public GameObject[] allGun;

    //定义子弹的容器
    public Transform bulletHolder;

    //获得同一种枪的不同等级子弹
    public GameObject[] bullet1;
    public GameObject[] bullet2;
    public GameObject[] bullet3;
    public GameObject[] bullet4;
    public GameObject[] bullet5;

    //当前为几号子弹
    public int costIndex = 0;
    //每发子弹所消耗的金币和造成的伤害值
    private int[] oneShootCost = {5,10,20,30,40,50,60,70,80,90,100,200,300,400,500,600,700,800,900,1000 };
    public string[] lvName = {"新手","入门","青铜","白银","黄金","铂金","钻石","大师","王者" };

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        //读取数据
        gold= PlayerPrefs.GetInt("gold",gold);
        exp = PlayerPrefs.GetInt("exp", exp);
        smallTimer = PlayerPrefs.GetFloat("scd", smallCountDown);
        Lv = PlayerPrefs.GetInt("lv", Lv);
        bigTimer = PlayerPrefs.GetFloat("bcd", bigCountDown);
        UpdateUI();
    }

    void Update()
    {
        ChangeBulletCost();
        OnFire();
        UpdateUI();
        ChangeBg();
    }

    /// <summary>
    /// 更新UI数据
    /// </summary>
    void UpdateUI()
    {
        //屏幕时间倒计时
        bigTimer -= Time.deltaTime;
        smallTimer -= Time.deltaTime;
        if (smallTimer<=0)
        {
            //重新归零并奖励玩家50金币
            smallTimer = smallCountDown;
            gold += 50;
        }
        if (bigTimer<=0&&priceButton.gameObject.activeSelf==false)
        {
            //将时间隐藏显示奖金按钮
            bigTimerCutText.gameObject.SetActive(false);
            priceButton.gameObject.SetActive(true);
        }
        //经验等级换算公式,升级所需经验=1000+200*当前等级
        while (exp>=1000+200*Lv)
        {
            exp = exp - (1000+200*Lv);
            Lv++;
            OverLv.SetActive(true);
            OverLv.transform.Find("Text").GetComponent<Text>().text = Lv.ToString();
            StartCoroutine(OverLv.GetComponent<HideSelf>().HideSelfs(0.5f));
            AudioManager.Instance.PlayMus(AudioManager.Instance.lvUpClip);
            Instantiate(lvEffect);
            
        }

        goldText.text = "$" + gold;
        lvText.text = Lv.ToString();
        if ((Lv/10)<=8)
        {
            lvNameText.text = lvName[Lv/10];
        }
        else
        {
            lvNameText.text = lvName[8];
        }
        smallTimerCutText.text = " " + (int)smallTimer / 10 + "   " + (int)smallTimer % 10;
        bigTimerCutText.text = (int)bigTimer + "s";
        expSlider.value = ((float)exp) / (1000 + 100 * Lv);
    }


    /// <summary>
    /// 更换背景
    /// </summary>
    void ChangeBg()
    {
        if (bgIndex!=Lv/20)
        {
            bgIndex = Lv / 20;
            AudioManager.Instance.PlayMus(AudioManager.Instance.seaWaveClip);
            Instantiate(seaWaveEffect);
            if (bgIndex>=3)
            {
                bgImage.sprite = bgSprite[3];
            }
            else
            {
                bgImage.sprite = bgSprite[bgIndex];

            }
           
        }
    }
    



    //开火
    void OnFire()
    {
        GameObject[] useBullet=bullet1;
        int bulletIndex;
        if (Input.GetMouseButtonDown(0)&&EventSystem.current.IsPointerOverGameObject()==false)
        {
            if (gold-oneShootCost[costIndex]>=0)
            {
                switch (costIndex / 4)
                {
                    case 0: useBullet = bullet1; break;
                    case 1: useBullet = bullet2; break;
                    case 2: useBullet = bullet3; break;
                    case 3: useBullet = bullet4; break;
                    case 4: useBullet = bullet5; break;
                }
                bulletIndex = (Lv % 10 >= 9) ? 9 : Lv % 10;
                AudioManager.Instance.PlayMus(AudioManager.Instance.fireClip);
                Instantiate(fireEffect);
                //实例化子弹
                GameObject bullet = Instantiate(useBullet[bulletIndex]);

                bullet.transform.SetParent(bulletHolder, false);
                //让子弹生成在开火位置
                bullet.transform.position = allGun[costIndex / 4].transform.Find("FirePos").transform.position;
                //让子弹的旋转角度跟枪旋转角度保持一致
                bullet.transform.rotation = allGun[costIndex / 4].transform.rotation;
                bullet.GetComponent<GunProPerty>().damage = oneShootCost[costIndex];
                bullet.gameObject.AddComponent<Ef_AutoMove>().dir = Vector3.up;
                bullet.GetComponent<Ef_AutoMove>().speed = bullet.GetComponent<GunProPerty>().speed;

            }
            else
            {
                StartCoroutine(GoldNotEnough());
            }
            
                
        }
    }
    //通过鼠标滚轮换枪
    void ChangeBulletCost()
    {
        if (Input.GetAxis("Mouse ScrollWheel")> 0)
        {
            ButtonPDown();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ButtonMDown();
        }
    }

    //换枪
    public void ButtonPDown()
    {
        allGun[costIndex / 4].SetActive(false);
        costIndex++;
        AudioManager.Instance.PlayMus(AudioManager.Instance.changeClip);
        Instantiate(changeEffect);
        costIndex = (costIndex > oneShootCost.Length - 1) ? 0 : costIndex;
        allGun[costIndex / 4].SetActive(true);
        oneShotCostText.text ="$"+ oneShootCost[costIndex];
    }

    public void ButtonMDown()
    {
        allGun[costIndex / 4].SetActive(false);
        costIndex--;
        AudioManager.Instance.PlayMus(AudioManager.Instance.changeClip);
        Instantiate(changeEffect);
        costIndex = (costIndex < 0) ? oneShootCost.Length - 1 : costIndex;
        allGun[costIndex / 4].SetActive(true);
        oneShotCostText.text = "$" + oneShootCost[costIndex];
    }
    /// <summary>
    /// 按下奖励按钮
    /// </summary>
    public void OnPeiceButtonDown()
    {
        gold += 500;
        AudioManager.Instance.PlayMus(AudioManager.Instance.rewardClip);
        Instantiate(goldEffect);
        priceButton.gameObject.SetActive(false);
        bigTimerCutText.gameObject.SetActive(true);
        bigTimer = bigCountDown;
    }
    /// <summary>
    /// 金币不足提示
    /// </summary>
    IEnumerator GoldNotEnough()
    {
        goldColor = goldText.color;
        goldText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        goldText.color = goldColor;

    }
}
