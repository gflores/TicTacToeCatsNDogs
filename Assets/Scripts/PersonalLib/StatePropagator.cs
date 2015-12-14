using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatePropagator : MonoBehaviour {
	public List<GameObject> targets;

	void OnDisable(){
		foreach (var target in targets) {
			target.SetActive(false);
		}
	}

	void OnEnable(){
		foreach (var target in targets) {
			target.SetActive(true);
		}
	}

}
