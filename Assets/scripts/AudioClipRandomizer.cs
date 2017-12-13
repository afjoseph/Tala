using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipRandomizer : MonoBehaviour {
	public AudioClip[] clips;
	// Use this for initialization
	void Start () {
		var audioSource = gameObject.GetComponent<AudioSource> ();
		var index = Random.Range (0, clips.Length);
		audioSource.clip = clips[index];
		audioSource.Play ();
	}
}