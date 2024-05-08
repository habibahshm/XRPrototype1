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
    [SerializeField] private Material _sceneMaterial;
   
     
    
    public MRUKAnchor.SceneLabels Labels = ~(MRUKAnchor.SceneLabels)0;
    private GameObject obj;
    private SliderLogic _sliderLogic;
    private AudioSource _music;
    private Light _spotLight;
    private ScreenControl _screen;
    
    private const string HighLightAttenuationShaderPropertyName = "_HighLightAttenuation";
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

        _sliderLogic = obj.GetComponentInChildren<SliderLogic>();
        _music = obj.GetComponentInChildren<AudioSource>();
        _spotLight = obj.transform.Find("SpotLight").GetComponent<Light>();
        _screen = obj.GetComponentInChildren<ScreenControl>();

    }

    private void Update()
    {
        if (obj)
        {
            float ratio = _sliderLogic.sliderRatio;
            _passthrough.SetBrightnessContrastSaturation(-1*ratio);
            _sceneMaterial.SetFloat(HighLightAttenuationShaderPropertyName, ratio);
            _spotLight.intensity = ratio / 2;
            if (ratio > 0.2 && !_music.isPlaying)
            {
                Debug.Log("play");
                _music.Play();
            }
            if(_music.isPlaying)
                _music.volume = ratio;
            if (ratio < 0.2)
            {
                _music.Pause();
            }
            
            _screen.setOpacity(1-ratio);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if(obj)
            Gizmos.DrawLine(obj.transform.position, targetToFace.position);
    }
}
