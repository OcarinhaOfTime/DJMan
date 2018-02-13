using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSampler : MonoBehaviour {
    [Range(6, 12)]
    public int bandsCount = 8;
    private int samplesCount;
    AudioSource audioSource;
    private float[] samples;
    [HideInInspector] public float[] bands;
    public float smoothTime = 10;

	AudioMixer mixer;

	private void Awake(){
        samplesCount = (int)Mathf.Pow(2, bandsCount + 1);
        samples = new float[samplesCount];
        bands = new float[bandsCount];

        audioSource = GetComponent<AudioSource>();

	}

	private void Update(){
		audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
		int begin = 0;

		for(int i=0; i<bands.Length; i++){
			int range = (int)Mathf.Pow(2, 1 + (bands.Length - i));
			var count = samples.Length / range;
			var sum = 0f;
			for(int j=begin; j<begin+count; j++){
				sum += samples[j];
			}

			bands[i] = Mathf.Lerp(bands[i], sum / (count - begin), smoothTime * Time.deltaTime);
			begin += count;
		}
	}
}
