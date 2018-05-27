
using UnityEngine;

public class GunFollow : MonoBehaviour {

    //获得当前的UGUICanvas
    public RectTransform UGUICanvas;

    //获取当前的摄像机
    public Camera mainCamera;



	void Update () {
        //获取鼠标在当前Canvas下的点击位置并转换为世界坐标
        Vector3 mousePos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(UGUICanvas,new Vector2(Input.mousePosition.x,Input.mousePosition.y),mainCamera,out mousePos);

        //获取枪的旋转角度
        float z;
        if (mousePos.x>transform.position.x)
        {
            z=-Vector3.Angle(Vector3.up,mousePos-transform.position);
        }
        else
        {
            z = Vector3.Angle(Vector3.up,mousePos-transform.position);
        }
        transform.localRotation = Quaternion.Euler(0,0,z);
		
	}
}
