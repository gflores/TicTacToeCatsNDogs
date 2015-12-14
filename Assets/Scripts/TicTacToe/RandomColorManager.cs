using UnityEngine;
using System.Collections;

public class RandomColorManager : MonoBehaviour {
	public static RandomColorManager instance {get; set;}
	// Use this for initialization
	void Awake () {
		instance = this;
	}

	public Color GetRandomCatColor(){
		Color color = new Color (1, 1, 1);
		color.r = Random.Range (0.5f, 0.7f);
		color.g = Random.Range (0.7f, 1f);
		color.b = Random.Range (0.8f, 1f);
		return color;
	}

	public Color GetRandomDogColor(){
		Color color = new Color (1, 1, 1);
		color.r = Random.Range (0.8f, 1f);
		color.g = Random.Range (0.7f, 1f);
		color.b = Random.Range (0.5f, 0.7f);
		return color;
	}
}
