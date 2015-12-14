using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {
	public GameObject settingsMenuScreen;

	public void Launch1PlayerMode()
	{
		Application.LoadLevel("TicTacToeMain");
	}

	public void Launch2PlayersMode()
	{
		Application.LoadLevel("TicTacToeMain");
	}

	public void LaunchSettings()
	{
		settingsMenuScreen.SetActive (true);
	}
	public void BackFromSettings()
	{
		settingsMenuScreen.SetActive (false);
	}

	public void LaunchExit()
	{
		Application.Quit();
	}
}
