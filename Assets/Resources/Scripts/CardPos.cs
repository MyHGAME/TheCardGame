using UnityEngine;
using System.Collections;

public class CardPos : MonoBehaviour {
	//卡的位置
	public int MapX=0,MapY=0;
	public float hight=-5.5f;
	//可以移动的卡包括未翻的，道具卡，陷阱卡，后期增加判断隐形的卡
	public GameObject ThisMoveCard,ThisPropCard;

	// Use this for initialization
	void Start () {
		CardPosIns ();

	}
	void CardPosIns(){
		//初始化卡的位置
		float posx = this.gameObject.transform.position.x;
		float posz = this.gameObject.transform.position.z;
		this.gameObject.transform.position = new Vector3 (posx, hight, posz);
	}
	// Update is called once per frame
	void Update () {
	
	}

	void CardInfo(){

	}


}
