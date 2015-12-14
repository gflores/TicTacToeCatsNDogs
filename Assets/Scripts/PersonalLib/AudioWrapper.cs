using UnityEngine;
using System.Collections;

public class AudioWrapper : MonoBehaviour {
	public float initialVolume { get; set;}

	void Awake(){
		initialVolume = GetComponent<AudioSource> ().volume;
	}
}
