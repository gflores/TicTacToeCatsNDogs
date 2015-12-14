using UnityEngine;
using System.Collections;

public class CurrentPlayerTurnImg : MonoBehaviour {
	static  CurrentPlayerTurnImg _instance;
	static public CurrentPlayerTurnImg GetInstance(){
		return _instance;
	}

	public GameObject cat;
	public GameObject dog;
	public Animator catAnimator;
	public Animator dogAnimator;

	Vector3 _initialPosition;

	void Awake()
	{
		_instance = this;
		_initialPosition = transform.position;
	}
	void SetCatsAndDogAnimation(){
		catAnimator.SetBool ("IsCat", true);
		dogAnimator.SetBool ("IsCat", false);
	}
	public void SetPlayerTurnImg(PlayerType playerTurn){
		ReinitializePosition ();
		gameObject.SetActive(true);

		if (playerTurn == PlayerType.Cat) {
			catAnimator.GetComponent<SpriteRenderer>().sprite = null;
//			catAnimator.GetComponent<SpriteRenderer>().color = RandomColorManager.instance.GetRandomCatColor();
			cat.SetActive (true);
			dog.SetActive (false);
			SetCatsAndDogAnimation();
			StartCoroutine(PlayerChangeTransition(catAnimator));
		} else {
			dogAnimator.GetComponent<SpriteRenderer>().sprite = null;
//			dogAnimator.GetComponent<SpriteRenderer>().color = RandomColorManager.instance.GetRandomDogColor();
			dog.SetActive (true);
			cat.SetActive (false);
			SetCatsAndDogAnimation();
			StartCoroutine(PlayerChangeTransition(dogAnimator));
       }
	}
	public void SetHappy(){
		catAnimator.SetBool ("IsHappy", true);
		dogAnimator.SetBool ("IsHappy", true);
	}
	IEnumerator PlayerChangeTransition(Animator animator){
		if (TicTacToeManager.GetInstance ().currentPlayerTurn == PlayerType.Cat) {
			SoundManager.instance.PlayIndependant(SoundManager.instance.cat_sound);
		} else {
			SoundManager.instance.PlayIndependant(SoundManager.instance.dog_sound);
		}
		animator.SetBool ("IsHappy", false);
		yield return new WaitForSeconds (0.1f);
		animator.SetBool ("IsHappy", true);
		yield return new WaitForSeconds (0.35f);
		animator.SetBool ("IsHappy", false);
	}

	public void ReinitializePosition()
	{
		transform.position = _initialPosition;
	}
}
