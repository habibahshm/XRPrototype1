using System;
using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class TeleLogic : MonoBehaviour
{
    [SerializeField] Transform targetToFace;
    
    private void Start()
    {
        Vector3 alignVector = Vector3.ProjectOnPlane(targetToFace.position - transform.position, Vector3.up);
        transform.forward = alignVector;
    }
    
}
