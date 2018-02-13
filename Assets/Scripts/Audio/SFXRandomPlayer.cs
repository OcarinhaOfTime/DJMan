using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXRandomPlayer : MonoBehaviour {
    public AudioClip[] clips;
    private AudioSource audioSource;
    private ShufflerQueue<AudioClip> queue;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        queue = new ShufflerQueue<AudioClip>(clips);
    }

    public void Play() {
        if (!audioSource.isPlaying) {
            audioSource.clip = queue.next;
            audioSource.Play();
        }
    }
}
