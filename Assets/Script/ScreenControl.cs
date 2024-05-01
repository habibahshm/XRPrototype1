using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenControl : MonoBehaviour
{
    public void setOpacity(float ratio)
    {
        Renderer renderer = GetComponent<MeshRenderer>();
        Color color = renderer.materials[0].color;
        color.a = ratio;
        renderer.materials[0].color = color;
    }
}
