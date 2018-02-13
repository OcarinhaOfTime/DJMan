using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalAudioVisualizer : MonoBehaviour {
    public float corrector = 10;
    public Vector2 spacing = new Vector2(0.2f , 0.2f);
    public int bandHeight = 5;
	public GameObject prefab;
	public AudioSampler sampler;
	private Transform[,] samples;
    public Camera cam;
    public float minAmp = .05f;

    void Start () {
        samples = new Transform[sampler.bandsCount, bandHeight];

        var screenSize = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        var bSize = screenSize.x * 2 / sampler.bandsCount - spacing.x;
		var start = - screenSize.x + bSize;

		for(int i=0; i<sampler.bandsCount; i++){
            for(int j=0; j<bandHeight; j++) {
                var inst = Instantiate(prefab);
                inst.transform.SetParent(transform);
                inst.transform.position = new Vector2(start + (bSize + spacing.x) * i, j * spacing.y);
                inst.transform.localScale = new Vector3(bSize, inst.transform.localScale.y, 1);
                inst.name = "sample" + i;
                inst.SetActive(true);
                samples[i, j] = inst.transform;
            }			
		}
	}	

	void Update(){
        for (int i = 0; i < sampler.bandsCount; i++) {
            int amp = Mathf.CeilToInt(sampler.bands[i] * corrector * sampler.bandsCount - minAmp);

            for (int j = 0; j < bandHeight; j++) {
                samples[i, j].gameObject.SetActive(j < amp);
            }
        }
    }
}
