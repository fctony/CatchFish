
using UnityEngine;

public class Ef_AutoMove : MonoBehaviour {

    public float speed = 2f;

    public Vector3 dir = Vector3.right;

	void Update () {
        transform.Translate(dir * speed * Time.deltaTime);
	}
}
