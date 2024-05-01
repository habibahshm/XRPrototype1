using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderLogic : MonoBehaviour
{
    private Vector3 intialPos;
    public float sliderRatio;
    
    void Start()
    {
        intialPos = transform.parent.InverseTransformPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rel_pos = transform.parent.InverseTransformPoint(transform.position);
        float deltaZ = intialPos.z - rel_pos.z;
        sliderRatio = deltaZ / 0.25f;
    }
}
