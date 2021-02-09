using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LibraryDLL;

public class CCD : MonoBehaviour {

    public GameObject[] joints;
    public GameObject target;
    //// The target for the IK system
    //public GameObject targ;
    public GameObject GetChilds_Tentacle;

    // To store the position of the target
    //private float[] tpos;
    private Vector3 tpos;
    // Max number of tries before the system gives up (Maybe 10 is too high?)
    [SerializeField]
    private int Mtries = 10;
    // The number of tries the system is at now
    [SerializeField]
    private int tries = 0;

    private bool done = false;

    // the range within which the target will be assumed to be reached
    private float epsilon = 0.1f;

    private float[] _sin, _cos, _theta;
    private MyLibrary mL;
    void Start()
    {
        mL = new MyLibrary();
        //joints = new GameObject[51];
       // tpos = new float[] { target.transform.position.x, target.transform.position.y, target.transform.position.z };
        tpos = target.transform.position;
        //Function that get all the every single child of each gameobject
        //to not worry about the data joints in the editor.

        for (int i = 0; i < joints.Length; i++)
        {
            //Debug.Log(GetChilds_Tentacle.transform.GetChild(0).name);
            joints[i] = GetChilds_Tentacle.transform.GetChild(0).gameObject;
            GetChilds_Tentacle = GetChilds_Tentacle.transform.GetChild(0).gameObject;           

        }

        _cos = new float[joints.Length];
        _sin = new float[joints.Length];
        _theta = new float[joints.Length];
      
    }

    void Update()
    {
        if (!done)
        {
            // if the Max number of tries hasn't been reached
            if (tries <= Mtries)
            {
                for (int i = joints.Length - 2; i >= 0; i--)
                {
                    //float[] rotAct = { joints[i].transform.rotation.x, joints[i].transform.rotation.y, joints[i].transform.rotation.z, joints[i].transform.rotation.w};

                    Vector3 r1 = joints[joints.Length - 1].transform.position - joints[i].transform.position;
                    Vector3 r2 = target.transform.position - joints[i].transform.position;

                    float[] _r1 = { r1.x, r1.y, r1.z};
                    float[] _r2 = { r2.x, r2.y, r2.z };

                    float[] rot = mL.CalculateOctopusRotations(_r1, _r2, _cos[i], _sin[i], _theta[i]);
                    joints[i].transform.rotation = new Quaternion(rot[0], rot[1], rot[2], rot[3]) * joints[i].transform.rotation;
 
                }

                // increment tries
                tries++;
            }
        }  

        // find the difference in the positions of the end effector and the target
        // (there's obviously a more beautiful and DRY way to do this)
        float x = Mathf.Abs(joints[joints.Length - 1].transform.position.x - target.transform.position.x);
        float y = Mathf.Abs(joints[joints.Length - 1].transform.position.y - target.transform.position.y);
        float z = Mathf.Abs(joints[joints.Length - 1].transform.position.z - target.transform.position.z);

        // if target is within reach (within epsilon) then the process is done
        if (x < epsilon && y < epsilon && z < epsilon)
        {
            done = true;
        }
        // if it isn't, then the process should be repeated
        else
        {
            done = false;
        }

        //// the target has moved, reset tries to 0 and change tpos
        //if (target.transform.position.x != tpos[0] || target.transform.position.y != tpos[1] || target.transform.position.z != tpos[2])
        if (target.transform.position.x != tpos.x || target.transform.position.y != tpos.y || target.transform.position.z != tpos.z)
            {
            tries = 0;
            tpos = target.transform.position;
            //tpos = new float[] { target.transform.position.x, target.transform.position.y, target.transform.position.z };
        }


    }

    ////// function to convert an angle to its simplest form (between -pi to pi radians)
    double SimpleAngle(double theta)
    {
        theta = theta % (2.0 * Mathf.PI);
        if (theta < -Mathf.PI)
            theta += 2.0 * Mathf.PI;
        else if (theta > Mathf.PI)
            theta -= 2.0 * Mathf.PI;
        return theta;
    }
}
