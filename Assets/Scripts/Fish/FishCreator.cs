using System.Collections;
using UnityEngine;

public class FishCreator : MonoBehaviour {
    //鱼的生成位置
    public Transform[] genPos;
    //鱼的预制体
    public GameObject[] fishPrefabs;
    //存放鱼的容器
    public Transform fishHodler;
    //每一波鱼生成的时间间隔
    public float genFishTime = 0.3f;
    //每条鱼生成的时间间隔
    public float genFishWaitTime = 0.5f;
	void Start () {
        InvokeRepeating("CreateFish", 0, genFishTime);

    }

    //生成鱼
    void CreateFish()
    {
        int genPosIndex = Random.Range(0,genPos.Length);
        int fishPreIndex = Random.Range(0,fishPrefabs.Length);
        int maxNum = fishPrefabs[fishPreIndex].GetComponent<FishProPerty>().maxNum;
        float maxSpeed = fishPrefabs[fishPreIndex].GetComponent<FishProPerty>().maxSpeed;
        int num = Random.Range((maxNum / 2) + 1, maxNum);
        float speed = Random.Range(maxSpeed/2,maxSpeed);
        int moveType = Random.Range(0,2);//伪随机，0：直走；1：拐弯
        int angOffset;  //直走生效，直走转弯角
        int angSpeed;   //转弯生效，转弯角速度

        if (moveType==0)
        {
            angOffset = Random.Range(-22, 22);
            StartCoroutine( GenStraightFish(genPosIndex, fishPreIndex, num, speed, angOffset));
        }
        else
        {
            if (Random.Range(0,2)==0)   //判断是否取负的角速度
            {
                angSpeed = Random.Range(-15, -9);
            }
            else
            {
                angSpeed = Random.Range(9,15);
            }
            StartCoroutine(GenTurnFish(genPosIndex,fishPreIndex,num,speed,angSpeed));

        }
    }
    //生成直行的鱼
    IEnumerator GenStraightFish(int genPosIndex,int fishPreIndex,int num,float speed,int angOffset)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[fishPreIndex]);
            fish.transform.SetParent(fishHodler, false);
            fish.transform.localPosition = genPos[genPosIndex].localPosition;
            fish.transform.localRotation = genPos[genPosIndex].localRotation;
            fish.transform.Rotate(0, 0, angOffset);
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            fish.AddComponent<Ef_AutoMove>().speed = speed;
            yield return new WaitForSeconds(genFishWaitTime);
        }
    }

    //生成转弯的鱼
    IEnumerator GenTurnFish(int genPosIndex, int fishPreIndex, int num, float speed, int angSpeed)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[fishPreIndex]);
            fish.transform.SetParent(fishHodler, false);
            fish.transform.localPosition = genPos[genPosIndex].localPosition;
            fish.transform.localRotation = genPos[genPosIndex].localRotation;
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            fish.AddComponent<Ef_AutoMove>().speed = speed;
            fish.AddComponent<Ef_AutoRotate>().speed = angSpeed;
            yield return new WaitForSeconds(genFishWaitTime);
        }
    }
}
