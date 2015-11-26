using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {
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
	}

	public void LaunchExit()
	{
		Application.Quit();
	}
}
