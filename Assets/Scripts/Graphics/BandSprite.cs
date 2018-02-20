using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandSprite : MonoBehaviour {
    public Material bandSpriteMat;
    public AudioSampler sampler;
    public Color[] colors = new Color[8];

    void Awake() {
        bandSpriteMat.SetColorArray("_BandColors", colors);
    }

    void Update () {
        bandSpriteMat.SetColorArray("_BandColors", colors);
        bandSpriteMat.SetFloatArray("_Band", sampler.bands);
    }
}
