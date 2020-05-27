using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class MinimapSystem 
{
    [System.Serializable]
    public struct MinimapSystemSetting
    {
        public float heightStep;
        public float halfSize;
        public float wallThickness;
        public Material material;
        public bool isFixed;
    }

    static int s_WallThicknessId;

    static MinimapSystem()
    {
        s_WallThicknessId = Shader.PropertyToID("_WallThickness");
    }

    public static void Render(RenderTexture renderTarget,Vector3 origin,Vector3 forward,MinimapSystemSetting settings)
    {
        settings.material.SetFloat(s_WallThicknessId, settings.wallThickness);
        float aspectRadio = renderTarget.width / (float)renderTarget.height;
        CommandBuffer buffer = new CommandBuffer();
        Matrix4x4 lookAt;
        Vector3 camPos = origin + Vector3.up * 5.0f;
        if (settings.isFixed)
        {
            lookAt = Matrix4x4.TRS(camPos, Quaternion.LookRotation(Vector3.down, Vector3.forward), new Vector3(1, 1, -1)).inverse;
        }
        else
        {
            lookAt = Matrix4x4.TRS(camPos, Quaternion.LookRotation(Vector3.down, forward), new Vector3(1, 1, -1)).inverse;
        }
        buffer.SetRenderTarget(renderTarget);
        buffer.SetProjectionMatrix(Matrix4x4.Ortho(-settings.halfSize * aspectRadio, settings.halfSize * aspectRadio, -settings.halfSize, settings.halfSize, 0.5f, 1.5f));
        buffer.SetViewMatrix(lookAt);

        buffer.ClearRenderTarget(true, true, Color.black);
        foreach(var r in MinimapElements.Renderers)
        {
            buffer.DrawRenderer(r, settings.material);
        }

        Graphics.ExecuteCommandBuffer(buffer);
    }
}
