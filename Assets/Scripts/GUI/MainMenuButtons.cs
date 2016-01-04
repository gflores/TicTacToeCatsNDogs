using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {
	public GameObject settingsMenuScreen;
	public GameObject selectPlayer1Screen;

	public void Launch1PlayerMode()
	{
		Player1Mode.instance.isOnePlayerMode = true;
		selectPlayer1Screen.SetActive(true);
	}

	public void BackFromSelectPlayer1Screen(){
		selectPlayer1Screen.SetActive(false);
	}

	public void SelectPlayer1Cat(){
		Player1Mode.instance.playerOneIsCat = true;
		SoundManager.instance.PlayIndependant (SoundManager.instance.cat_sound);
		Invoke ("LaunchGame", 1.3f);
	}

	public void SelectPlayer1Dog(){
		Player1Mode.instance.playerOneIsCat = false;
		SoundManager.instance.PlayIndependant (SoundManager.instance.dog_sound);
		Invoke ("LaunchGame", 1.3f);
	}

	public void LaunchGame(){
		Application.LoadLevel("TicTacToeMain");
	}
	
	public void Launch2PlayersMode()
	{
		Player1Mode.instance.isOnePlayerMode = false;
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
