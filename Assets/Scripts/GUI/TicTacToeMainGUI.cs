using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TicTacToeMainGUI : MonoBehaviour {
	static public TicTacToeMainGUI instance { get; set;}
	public GameObject winnerScreen;
	public Text winnerText;

	public GameObject mainGameplayScreen;
	public GameObject afterWinScreen;
	public GameObject DrawScreen;
	public GameObject settingsMenu;

	void Awake(){
		instance = this;
	}

	public void LaunchWinnerScreen(){
		winnerScreen.gameObject.SetActive(true);
		mainGameplayScreen.SetActive (false);
		if (TicTacToeManager.GetInstance().winner == PlayerType.Cat)
			winnerText.text = "Cats win !";
		else
			winnerText.text = "Dogs win !";
		Invoke ("LaunchAfterWinScreen", 2f);
	}
	public void LaunchAfterWinScreen(){
		afterWinScreen.gameObject.SetActive(true);
	}

	public void LaunchDrawScreen(){
		DrawScreen.SetActive(true);
		mainGameplayScreen.SetActive(false);
		Destroy (CurrentPlayerTurnImg.GetInstance ().gameObject);
		Invoke ("LaunchAfterWinScreen", 2f);
	}

	public void PlayAgain(){
		Application.LoadLevel("TicTacToeMain");
	}
	public void BackToMainMenu(){
		Application.LoadLevel("MainMenu");
	}

	public void OpenSettingsMenu(){
		settingsMenu.SetActive (true);
	}
	public void CloseSettingsMenu(){
		settingsMenu.SetActive (false);
	}
}
