using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AudioCamera : MonoBehaviour {
    public const string TexName = "_GlobalAudioTex";

    private void OnEnable() {
        var cam = GetComponent<Camera>();

        RenderTexture rt = new RenderTexture(cam.pixelWidth, cam.pixelHeight, 0);
        rt.wrapMode = TextureWrapMode.Repeat;
        rt.filterMode = FilterMode.Point;
        if (cam.targetTexture != null) {
            var temp = cam.targetTexture;
            cam.targetTexture = null;
            DestroyImmediate(temp);
        }

        cam.targetTexture = rt;
        Shader.SetGlobalTexture(TexName, cam.targetTexture);
    }
}
