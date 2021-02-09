using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    /// <summary>
    /// Transform of the target
    /// </summary>
    [SerializeField]
    private Transform _target;
    /// <summary>
    /// slider of the force canvas
    /// </summary>
    [SerializeField]
    Slider _slider;
    /// <summary>
    /// force applied (angle) on the ball
    /// </summary>
    float _forceApplied;
    /// <summary>
    /// arrows of velocity and force
    /// </summary>
    [SerializeField]
    GameObject _arrowR, _arrowG;
    /// <summary>
    /// Properties of _forceApplied
    /// </summary>
    public float ForceApplied
    {
        get { return _forceApplied; }
        set { _forceApplied = value; }
    }
    Vector3 _initialPos;
    // Use this for initialization
    void Start()
    {
        _initialPos = this.transform.position;
        _forceApplied = _slider.value;
        
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //we applie the predicted velocity to the rigidbody's velocity
            this.GetComponent<Rigidbody>().velocity = BallisticVel(_target, _forceApplied);
        }
    }
    // Update is called once per frame
    void Update()
    {
        ArrowRed();
        ArrowGreen();
        
        if ((this.transform.position.x >= _target.position.x) || (Input.GetKeyUp(KeyCode.Escape))) // || poner si ha colisionado con el pulpo 
        {
            this.transform.position = _initialPos;
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }
    /// <summary>
    /// Function to calculate force's arrow size
    /// </summary>
    private void ArrowRed()
    {
        //set the rotation of the arrow into targets direction
        _arrowR.transform.LookAt(_target);
        //scale the z component in the localScale (its a child) to adjust the arrow in its force magnitude
        float _f = _forceApplied / 100;
        _arrowR.transform.localScale = new Vector3(_arrowR.transform.localScale.x, _arrowR.transform.localScale.y, _f/75); //we divde it for its maximum value
    }
    /// <summary>
    /// Function to calculate velocity's arrow size
    /// </summary>
    private void ArrowGreen()
    {
        //scale the z component in the localScale (its a child) to adjust the arrow in its velocity magnitude
        _arrowG.transform.localScale = new Vector3(_arrowR.transform.localScale.x, _arrowR.transform.localScale.y, this.GetComponent<Rigidbody>().velocity.magnitude / 100/30); //we divide it on 100 because the scale its to small and 50 to its maxim value 
    }
    /// <summary>
    /// Function to calculate the trajectory of the ball.
    /// To do it so, we calculate the velocity it just has.
    /// We got this calculations in a post on unity's forums
    /// </summary>
    private Vector3 BallisticVel(Transform _target, float _angle)
    {
        Vector3 _dir = _target.position - this.transform.position;  // get target direction
        float _h = _dir.y;  // get height difference
        _dir.y = 0f;  // retain only the horizontal direction
        float _distance = _dir.magnitude;  // get horizontal distance
        float _newAngle = _angle * Mathf.Deg2Rad;  // convert angle to radians
        _dir.y = _distance * Mathf.Tan(_newAngle);  // set dir to the elevation angle
        _distance += _h / Mathf.Tan(_newAngle);  // correct for small height differences
        // calculate the velocity magnitude
        float _vel = Mathf.Sqrt(_distance * Physics.gravity.magnitude / Mathf.Sin(2 * _newAngle));
        return _vel * _dir.normalized;
    }
}
