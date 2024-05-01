using System;
using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using Oculus.Interaction;
using UnityEngine;

public class TeleControl : MonoBehaviour
{
    [SerializeField] Transform targetToFace;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private OVRPassthroughLayer _passthrough;
    public MRUKAnchor.SceneLabels Labels = ~(MRUKAnchor.SceneLabels)0;
    private GameObject obj;
    public void StartSpawn()
    {
        var room = MRUK.Instance.GetCurrentRoom();
        var anchors = room.GetRoomAnchors();
        foreach (var item in anchors)
        {
            if (item.HasLabel(Labels.ToString()))
            {
                obj = Instantiate(objectToSpawn, item.transform.position, Quaternion.identity);
                obj.transform.SetParent(transform);
                Vector3 alignVector = Vector3.ProjectOnPlane(targetToFace.position -obj.transform.position, Vector3.up);
                obj.transform.forward = alignVector;
            }
        }
    }

    private void Update()
    {
        if (obj)
        {
            float ratio = obj.GetComponentInChildren<SliderLogic>().sliderRatio;
            _passthrough.textureOpacity = 1 - ratio;
            obj.GetComponentInChildren<ScreenControl>().setOpacity(1-ratio);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if(obj)
            Gizmos.DrawLine(obj.transform.position, targetToFace.position);
    }
}
