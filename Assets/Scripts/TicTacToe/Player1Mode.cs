using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player1Mode : MonoBehaviour {
	static public Player1Mode instance;

	public bool isOnePlayerMode;
	public bool playerOneIsCat;

	float waitTime = 1.5f;

	void Awake(){
		if (instance == null)
			instance = this;
		else
			Destroy (this.gameObject);
		DontDestroyOnLoad(this.gameObject) ;
	}
	void Start(){
		
	}

	void NotifyPlayerTurn(){

		StartCoroutine (Coroutine_NotifyPlayerTurn());
	}

	IEnumerator Coroutine_NotifyPlayerTurn(){
		List<TicTacToeCell> availableCells = new List<TicTacToeCell>();
		foreach(var cell in TicTacToeManager.GetInstance().cells){
			if (cell.isCellFree == true)
				availableCells.Add(cell);
		}
		if (availableCells.Count == 0)
			yield return null;
		else {

			TicTacToeMainGUI.instance.playerNotification.SetActive (true);
			yield return new WaitForSeconds (waitTime);
			TicTacToeMainGUI.instance.playerNotification.SetActive (false);
		}
	}

	void NotifyOpponentTurn(){
		StartCoroutine (Coroutine_NotifyOpponentTurn());
	}
	
	IEnumerator Coroutine_NotifyOpponentTurn(){
		List<TicTacToeCell> availableCells = new List<TicTacToeCell>();
		foreach(var cell in TicTacToeManager.GetInstance().cells){
			if (cell.isCellFree == true)
				availableCells.Add(cell);
		}
		if (availableCells.Count == 0)
			yield return null;
		else
		{
			TicTacToeMainGUI.instance.opponentNotification.SetActive (true);
			yield return new WaitForSeconds (waitTime);
			TicTacToeMainGUI.instance.opponentNotification.SetActive (false);

	//		yield return new WaitForSeconds (0.5f);


			TicTacToeCell randomCell = availableCells[Random.Range(0, availableCells.Count)];
			
			randomCell.ActivateCurrent();
		}

	}

	public void DisplayNotificationIfNeeded(){
		if (isOnePlayerMode == true) {
			if (TicTacToeManager.GetInstance().currentPlayerTurn == PlayerType.Cat && playerOneIsCat == true ||
			    TicTacToeManager.GetInstance().currentPlayerTurn == PlayerType.Dog && playerOneIsCat == false
			)
				NotifyPlayerTurn();
			else
			{
				TicTacToeManager.GetInstance().LockBoard ();
				NotifyOpponentTurn();
			}
		}
	}
}
