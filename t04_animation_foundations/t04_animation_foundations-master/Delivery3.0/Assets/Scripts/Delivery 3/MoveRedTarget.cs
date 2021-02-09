using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRedTarget : MonoBehaviour {

    public float speed;
    public GameObject RedTarget;

	void Update ()
    {
        float h = Input.GetAxis("Horizontal") * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * Time.deltaTime;

        this.transform.Translate(0, v * speed, -h * speed);

        RedTarget.transform.position = this.transform.position;

    }

}
