using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveYellowTarget : MonoBehaviour {

    public GameObject LeftTentacle;
    public GameObject RightTentacle;

    public GameObject LeftEffector;
    public GameObject RightEffector;

    public GameObject RedTarget;

    private float distanceLeft, distanceRight;

    private void Start()
    {
    }
    private void Update()
    {

        //Check which tentacle is near to the red target
        distanceLeft = Vector3.Distance(RedTarget.transform.position, LeftTentacle.transform.position);
        distanceRight = Vector3.Distance(RedTarget.transform.position, RightTentacle.transform.position);

        if (distanceLeft < distanceRight)
        {
            this.transform.position = LeftEffector.transform.position;
        }
        else
        {
            this.transform.position = RightEffector.transform.position;
        }
    }

}
