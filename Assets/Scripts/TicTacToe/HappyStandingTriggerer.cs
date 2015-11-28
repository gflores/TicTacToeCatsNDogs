using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HappyStandingTriggerer : MonoBehaviour {
	static public HappyStandingTriggerer instance;
	public List<TicTacToeCell> cellsToAnimate;
	Animator _animator;

	void Awake(){
		instance = this;
		cellsToAnimate = new List<TicTacToeCell> ();
		_animator = GetComponent<Animator> ();
		_animator.SetInteger("State", 1);
	}

	public void RegisterCellToAnimation(TicTacToeCell cell){
		cellsToAnimate.Add(cell);
	}
	public void LaunchHappyStanding(){
		Debug.LogWarning ("okay !!");
		foreach (var cell in cellsToAnimate){
			cell.StartHappyStandingAnimation ();
		}
		cellsToAnimate.Clear();
	}
}
