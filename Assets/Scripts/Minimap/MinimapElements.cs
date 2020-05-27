using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


/// <summary>
/// 任何需要渲染到小地图的物体都要添加此脚本
/// </summary>
public class MinimapElements : MonoBehaviour
{
    public static List<Renderer> Renderers => s_Renderers;

    static List<Renderer> s_Renderers = new List<Renderer>();

    Renderer m_Renderer;

    private void OnEnable()
    {
        m_Renderer = GetComponent<Renderer>();

        if (m_Renderer != null)
            s_Renderers.Add(m_Renderer);
    }

    private void OnDisable()
    {
        if (m_Renderer)
            s_Renderers.Remove(m_Renderer);
    }
}
