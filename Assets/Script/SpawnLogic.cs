using System;
using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class SpawnLogic : MonoBehaviour
{
    [SerializeField] Transform targetToFace;
    [SerializeField] private GameObject objectToSpawn;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if(obj)
            Gizmos.DrawLine(obj.transform.position, targetToFace.position);
    }
}
