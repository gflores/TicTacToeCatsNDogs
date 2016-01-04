using UnityEngine;
using System.Collections;

public class TicTacToeCell : MonoBehaviour {
	public GameObject normalCat;
	public GameObject normalDog;
	public GameObject happyCat;
	public GameObject happyDog;

	public Animator catAnimator;
	public Animator dogAnimator;

	public Animator catSpriteAnimator;
	public Animator dogSpriteAnimator;

	public bool isCellFree { get; set; }
	public PlayerType playerOwned { get; set; }
	public int coordX { get; set; }
	public int coordY { get; set; }

	public bool isFingerInsideCell { get; set;}
	SpriteRenderer _spriteRenderer;
	ShakeTransform _shakeTransform;
	//	public SpriteRenderer animalFaceSpriteRenderer { get; set;}

	void Awake(){
		_spriteRenderer = GetComponent<SpriteRenderer> ();
		_shakeTransform = GetComponent<ShakeTransform> ();
		isCellFree = true;
		isFingerInsideCell = false;
	}
	void Start(){
		catAnimator.SetBool("IsCat", true);
		catAnimator.SetInteger("State", 1);
		dogAnimator.SetBool("IsCat", false);
		dogAnimator.SetInteger("State", 1);
	}
	public void StartHappyStandingAnimation(){
//		if (playerOwned == PlayerType.Cat) {
//			catAnimator.SetBool("IsCat", true);
//			catAnimator.SetInteger("State", 1);
//		} else {
//			dogAnimator.SetBool("IsCat", false);
//			dogAnimator.SetInteger("State", 1);
//		}
	}

	IEnumerator LaunchActivateCurrent()
	{
		float moveTime = 0.5f;
		Hashtable h = new Hashtable ();

		isCellFree = false;
		playerOwned = TicTacToeManager.GetInstance().currentPlayerTurn;

		h.Add ("position", transform.position);
		h.Add ("time", moveTime);
		h.Add ("easetype", iTween.EaseType.easeInCubic);
		iTween.MoveTo(CurrentPlayerTurnImg.GetInstance().gameObject, h);
		CurrentPlayerTurnImg.GetInstance ().SetHappy ();
		yield return new WaitForSeconds(moveTime);
		PlayCurrentSound ();
		yield return null;
		CurrentPlayerTurnImg.GetInstance ().gameObject.SetActive(false);

		StartCoroutine (TicTacToeManager.GetInstance().mainCamera.GetComponent<ShakeTransform> ().LaunchSelf ());
		TicTacToeMainGUI.instance.playerNotification.SetActive (false);
		TicTacToeManager.GetInstance ().CheckWinningCondition ();


		if (TicTacToeManager.GetInstance ().currentPlayerTurn == PlayerType.Cat) {
			normalCat.SetActive (true);
			catSpriteAnimator.SetBool("IsHappy", true);
			catSpriteAnimator.SetBool("IsCat", true);
//			catSpriteAnimator.GetComponent<SpriteRenderer>().color = CurrentPlayerTurnImg.GetInstance().catAnimator.GetComponent<SpriteRenderer>().color;
		} else {
			normalDog.SetActive (true);
			dogSpriteAnimator.SetBool("IsHappy", true);
			dogSpriteAnimator.SetBool("IsCat", false);
//			dogSpriteAnimator.GetComponent<SpriteRenderer>().color = CurrentPlayerTurnImg.GetInstance().dogAnimator.GetComponent<SpriteRenderer>().color;
		}

		yield return StartCoroutine(_shakeTransform.LaunchSelf ());
		if (TicTacToeManager.GetInstance ().isGameFinished == true) {
			TicTacToeManager.GetInstance().LaunchVictoryDance();
		} else {
			catSpriteAnimator.SetBool ("IsHappy", false);
			dogSpriteAnimator.SetBool ("IsHappy", false);
			yield return new WaitForSeconds (0.8f);
			TicTacToeManager.GetInstance ().UnlockBoard ();
			TicTacToeManager.GetInstance ().AlternateTurn ();
		}
//		PlayCurrentSound ();
//		HappyStandingTriggerer.instance.RegisterCellToAnimation(this);
	}
	void PlayCurrentSound(){
		if (TicTacToeManager.GetInstance ().currentPlayerTurn == PlayerType.Cat) {
			SoundManager.instance.PlayIndependant(SoundManager.instance.cat_sound);
		} else {
			SoundManager.instance.PlayIndependant(SoundManager.instance.dog_sound);
		}
	}
	public void ActivateCurrent(){
		TicTacToeManager.GetInstance ().LockBoard ();
		StartCoroutine (LaunchActivateCurrent());
	}

	public void WinningCellEffect(){
		catAnimator.SetInteger("State", 2);
		dogAnimator.SetInteger("State", 2);
		catSpriteAnimator.SetBool ("IsHappy", true);
		dogSpriteAnimator.SetBool ("IsHappy", true);

		catAnimator.transform.parent.transform.localPosition = new Vector2 (-0.005f, 0);
		dogAnimator.transform.parent.transform.localPosition = Vector3.zero;

		//		if (playerOwned == PlayerType.Cat) {
//			normalCat.SetActive (false);
//			happyCat.SetActive (true);
//		} else {
//			normalDog.SetActive (false);
//			happyDog.SetActive (true);
//		}

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
			((Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Ended)
		 	
		 		|| Input.GetMouseButtonUp(0)
			
		 ) &&	isFingerInsideCell && TicTacToeManager.GetInstance().isBoardLocked() == false)
		{
			Debug.LogWarning("Click received !");
			OnTouchExit();
			ActivateCurrent ();
		}
		if (Input.touches.Length == 0)
			OnTouchExit ();
	}

	void OnMouseOver(){
		if (isFingerInsideCell == false && TicTacToeManager.GetInstance().isGameFinished == false &&
		    isCellFree == true && TicTacToeManager.GetInstance().isBoardLocked() == false) {
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
