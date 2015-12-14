using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlidersManager : MonoBehaviour {

	public Slider musicSlider;
	public Slider SFXSlider;

	void Start (){
		musicSlider.value = SoundManager.instance.musicVolumeRatio;
		SFXSlider.value = SoundManager.instance.SFXVolumeRatio;

		musicSlider.onValueChanged.AddListener (SoundManager.instance.ChangeVolumeForCurrentMusic);
		SFXSlider.onValueChanged.AddListener (SoundManager.instance.ChangeVolumeForSFX);
	}	
}
