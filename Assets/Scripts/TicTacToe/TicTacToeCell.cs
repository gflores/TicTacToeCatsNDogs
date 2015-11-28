using UnityEngine;
using System.Collections;

public class TicTacToeCell : MonoBehaviour {
	public GameObject normalCat;
	public GameObject normalDog;
	public GameObject happyCat;
	public GameObject happyDog;

	public Animator catAnimator;
	public Animator dogAnimator;

	public bool isCellFree { get; set; }
	public PlayerType playerOwned { get; set; }
	public int coordX { get; set; }
	public int coordY { get; set; }

	public bool isFingerInsideCell { get; set;}
	SpriteRenderer _spriteRenderer;
//	public SpriteRenderer animalFaceSpriteRenderer { get; set;}

	void Awake(){
		_spriteRenderer = GetComponent<SpriteRenderer> ();
		isCellFree = true;
		isFingerInsideCell = false;
	}

	public void StartHappyStandingAnimation(){
		if (playerOwned == PlayerType.Cat) {
			catAnimator.SetBool("IsCat", true);
			catAnimator.SetInteger("State", 1);
		} else {
			dogAnimator.SetBool("IsCat", false);
			dogAnimator.SetInteger("State", 1);
		}
	}

	void ActivateCurrent(){
		StartCoroutine (Camera.current.GetComponent<ShakeTransform> ().LaunchSelf ());
		isCellFree = false;
		playerOwned = TicTacToeManager.GetInstance().currentPlayerTurn;

		if (TicTacToeManager.GetInstance ().currentPlayerTurn == PlayerType.Cat) {
			normalCat.SetActive (true);
			SoundManager.instance.PlayIndependant(SoundManager.instance.cat_sound);
		} else {
			normalDog.SetActive (true);
			SoundManager.instance.PlayIndependant(SoundManager.instance.dog_sound);
		}
		TicTacToeManager.GetInstance ().CheckWinningCondition ();
		TicTacToeManager.GetInstance ().AlternateTurn ();
		HappyStandingTriggerer.instance.RegisterCellToAnimation(this);
	}

	public void WinningCellEffect(){
		if (playerOwned == PlayerType.Cat) {
			normalCat.SetActive (false);
			happyCat.SetActive (true);
		} else {
			normalDog.SetActive (false);
			happyDog.SetActive (true);
		}

	}

	void Update(){
//		if (Input.touches.Length > 0) {
//			Touch touch = Input.touches[0];
//			if (touch.phase == TouchPhase.Ended || Input.GetMouseButtonUp(0) && isFingerInsideCell)
//			{
//				OnTouchExit();
//				ActivateCurrent ();
//			}
//		}
		if (
			((Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Ended) /*|| Input.GetMouseButtonUp(0)*/) &&
		    isFingerInsideCell
		  	)
		{
			OnTouchExit();
			ActivateCurrent ();
		}
	}

	void OnMouseEnter(){
		if (TicTacToeManager.GetInstance().isGameFinished == false && isCellFree == true) {
			Color color = _spriteRenderer.color;
			color.a = 0.5f;
			_spriteRenderer.color = color;
			isFingerInsideCell = true;
		}
	}
	void OnMouseExit(){
		OnTouchExit ();
		isFingerInsideCell = false;
	}

	void OnTouchExit(){
		Color color = _spriteRenderer.color;
		color.a = 0.0f;
		_spriteRenderer.color = color;
	}
	//	void OnMouseUp(){
//		Color color = _spriteRenderer.color;
//		color.a = 0.0f;
//		_spriteRenderer.color = color;
//		ActivateCurrent ();
//		
//	}
}
