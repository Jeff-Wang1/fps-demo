using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public void ShakeCamera(float scale)
    {
        this.transform.Rotate(new Vector3(-1.0f * scale, 0, 0), Space.Self);
    }
}
