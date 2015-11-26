using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	void Start(){
		SoundManager.instance.PlayIfDifferent (SoundManager.instance.mainMenuMusic);
	}
}
