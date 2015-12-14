using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerType {Cat, Dog};

public class TicTacToeManager : MonoBehaviour {
	static TicTacToeManager _instance;
	public static TicTacToeManager GetInstance(){
		return _instance;
	}
	List<TicTacToeCell> _potentialWinnerCells;

	int dimensionX = 3;
	int dimensionY = 3;

	public Camera mainCamera;

	public bool isGameFinished { get; set;}
	public PlayerType winner{ get; set;}
//	public GameObject normalCatModel;
//	public GameObject normalDogModel;
//	public GameObject happyCatModel;
//	public GameObject happyDogModel;

	public TicTacToeCell[] cells;
	int turnCount;
	public PlayerType currentPlayerTurn { get; set;}
	bool _isBoardLocked = false;

	void Awake(){
		_instance = this;

		turnCount = 0;
		_potentialWinnerCells = new List<TicTacToeCell> ();
		isGameFinished = false;
		for (int i = 0; i < dimensionX * dimensionY; ++i) {
			cells[i].coordX = i % dimensionX;
			cells[i].coordY = i / dimensionX;
		}
	}
	void Start()
	{
		SoundManager.instance.PlayIfDifferent (SoundManager.instance.mainGameMusic);
		InitGame ();
	}

	void InitGame(){
		SetPlayerTurn(PlayerType.Cat);
	}

	public void LockBoard(){
		_isBoardLocked = true;
	}
	public void UnlockBoard(){
		_isBoardLocked = false;
	}
	public bool isBoardLocked(){
		return _isBoardLocked;
	}

	void Update(){

	}

	public void CheckWinningCondition(){
		++turnCount;
		if (CheckIfWon (PlayerType.Cat) == true) {
			ShowWinner(PlayerType.Cat);
		} else if (CheckIfWon (PlayerType.Dog) == true) {
			ShowWinner(PlayerType.Dog);
		} else if (turnCount == dimensionX * dimensionY)
			TicTacToeMainGUI.instance.LaunchDrawScreen();
		else
			EmptyPotentialWinnerCells();
	}
	void ShowWinner(PlayerType player)
	{
		isGameFinished = true;
		winner = player;
		CurrentPlayerTurnImg.GetInstance ().gameObject.SetActive (false);
		TicTacToeMainGUI.instance.LaunchWinnerScreen();
	}

	public void LaunchVictoryDance(){
		foreach (var cell in _potentialWinnerCells) {
			cell.WinningCellEffect();
		}
	}
	bool CheckIfWon(PlayerType player){
		for (int i = 0; i < dimensionX; ++i) {
			if (CheckIfWonVertical(player, i) == true)
				return true;
		}
		for (int i = 0; i < dimensionY; ++i) {
			if (CheckIfWonHorizontal(player, i) == true)
				return true;
		}
		if (CheckIfWonDiagonal1(player) == true)
			return true;
		if (CheckIfWonDiagonal2(player) == true)
			return true;
		return false;
	}
	void EmptyPotentialWinnerCells(){
		_potentialWinnerCells.Clear();
	}
	void AddToPotentialWinnerCells(TicTacToeCell cell){
		_potentialWinnerCells.Add(cell);
	}
	bool CheckIfWonVertical(PlayerType player, int x)
	{
		EmptyPotentialWinnerCells();
		for (int y = 0; y < dimensionY; ++y) {
			if (CheckIfCellIsOwnedByPlayer(player, x, y) == false)
				return false;
		}
		return true;
	}
	bool CheckIfWonHorizontal(PlayerType player, int y)
	{
		EmptyPotentialWinnerCells();
		for (int x = 0; x < dimensionX; ++x) {
			if (CheckIfCellIsOwnedByPlayer(player, x, y) == false)
				return false;
		}
		return true;
	}
	bool CheckIfWonDiagonal1(PlayerType player)
	{
		EmptyPotentialWinnerCells();
		for (int i = dimensionX - 1; i > -1; --i) {
			if (CheckIfCellIsOwnedByPlayer(player, dimensionX - i - 1, i) == false)
				return false;
		}
		return true;
	}
	bool CheckIfWonDiagonal2(PlayerType player)
	{
		EmptyPotentialWinnerCells();
		for (int i = dimensionX - 1; i > -1; --i) {
			if (CheckIfCellIsOwnedByPlayer(player, i, i) == false)
				return false;
		}
		return true;
	}


	bool CheckIfCellIsOwnedByPlayer(PlayerType player, int x, int y)
	{
		TicTacToeCell cell = GetCell(x, y);
		AddToPotentialWinnerCells(cell);
		return cell.isCellFree == false && cell.playerOwned == player;
	}

	TicTacToeCell GetCell(int x, int y){
		return cells[dimensionX * y + x];
	}
	public void AlternateTurn(){
		SetPlayerTurn (currentPlayerTurn == PlayerType.Cat ? PlayerType.Dog : PlayerType.Cat);
	}

	void SetPlayerTurn( PlayerType playerTurn){
		currentPlayerTurn = playerTurn;
		CurrentPlayerTurnImg.GetInstance ().SetPlayerTurnImg (currentPlayerTurn);
	}
}
