
using UnityEngine;

public class Ef_AutoRotate : MonoBehaviour {

    public float speed;
	void Update () {
        transform.Rotate(Vector3.forward,speed*Time.deltaTime);
	}
}
