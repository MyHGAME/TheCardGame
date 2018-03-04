using UnityEngine;
using System.Collections;

public class AddCardButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void AddCard(){
		MapManager._this.LoadCardPos (CardArray._this.MyCardArray);
	} 
}
