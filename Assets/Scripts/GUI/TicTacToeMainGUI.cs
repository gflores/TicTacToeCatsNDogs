using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TicTacToeMainGUI : MonoBehaviour {
	static public TicTacToeMainGUI instance { get; set;}
	public GameObject winnerScreen;
	public Text winnerText;
	public GameObject afterWinScreen;
	public GameObject DrawScreen;

	void Awake(){
		instance = this;
	}

	public void LaunchWinnerScreen(){
		winnerScreen.gameObject.SetActive(true);
		if (TicTacToeManager.GetInstance().winner == PlayerType.Cat)
			winnerText.text = "Cats win !";
		else
			winnerText.text = "Dogs win !";
		LaunchAfterWinScreen();
	}
	public void LaunchAfterWinScreen(){
		afterWinScreen.gameObject.SetActive(true);
	}

	public void LaunchDrawScreen(){
		DrawScreen.SetActive(true);
	}

	public void PlayAgain(){
		Application.LoadLevel("TicTacToeMain");
	}
	public void BackToMainMenu(){
		Application.LoadLevel("MainMenu");
	}
}
