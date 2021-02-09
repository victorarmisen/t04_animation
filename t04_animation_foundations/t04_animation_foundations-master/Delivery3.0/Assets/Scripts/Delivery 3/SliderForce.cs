using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderForce : MonoBehaviour
{
    [SerializeField]
    GameObject _ball;
    Slider _force;

    // Use this for initialization
    void Start()
    {
        _force = this.GetComponent<Slider>();
        _ball.GetComponent<Ball>().ForceApplied = _force.value;
    }

    public void ChangeSliderValue()
    {
        _ball.GetComponent<Ball>().ForceApplied = _force.value;
    }
}
