using UnityEngine;
using System.Collections;

public class CurrentPlayerTurnImg : MonoBehaviour {
	static  CurrentPlayerTurnImg _instance;
	static public CurrentPlayerTurnImg GetInstance(){
		return _instance;
	}

	public GameObject cat;
	public GameObject dog;

	void Awake()
	{
		_instance = this;
	}
	
	public void SetPlayerTurnImg(PlayerType playerTurn){
		if (playerTurn == PlayerType.Cat) {
			cat.SetActive (true);
			dog.SetActive (false);
		} else {
			dog.SetActive (true);
			cat.SetActive (false);
		}
	}
}
