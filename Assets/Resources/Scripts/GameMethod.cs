using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMethod : MonoBehaviour {
	public enum GamePhase
	{
		PreparePhase,
		FlopPhase,
		MajorPhase,
		BattlePhase,
		MajorPhase2,
		EndPhase,
	};
	public GamePhase Phase=GamePhase.PreparePhase;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
