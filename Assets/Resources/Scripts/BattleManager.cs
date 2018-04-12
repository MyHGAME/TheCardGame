using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour {
	public GameObject CardArrayPos;
	public ArrayList MyCardArray=new ArrayList();
	public float CardDis=0.1f;
	public GameObject SelectedCard;

	// Use this for initialization
	void Start () {
		LoadCardArray ();
		ShowCardArray ();
	}
	//在决斗中载入自己的卡组
	void LoadCardArray(){
		MyCardArray = CardArray._this.MyCardArray;
	}
	//实例化自己的卡组
	void ShowCardArray(){
		if (MyCardArray != null) {
			float TempDis=0;
			float x=CardArrayPos.transform.position.x;
			float y=CardArrayPos.transform.position.y+0.1f;
			float z=CardArrayPos.transform.position.z;
			foreach(GameObject obj in MyCardArray){
				obj.transform.position=new Vector3(x,y-TempDis,z);
				TempDis-=CardDis;
			}
		}
		MapManager._this.LoadCardPos (MyCardArray);
	}

	void CleanCardArray(){

	}
	// Update is called once per frame
	void Update () {
	
	}
}
