using UnityEngine;
using System.Collections;


//卡组类:卡组的初始化，卡组检索，卡组视觉效果（卡组动画），分配卡，卡组分类，卡组储存，（卡组分游戏中的卡组，大厅的卡组）
//游戏中的卡组，卡牌的视觉效果，卡组的检索（主要是将卡组信息传到游戏中的处理类）
//大厅的卡组：用于玩家整理卡组（对卡组的操作），卡组的修改:添加，移除，标志等等
public class CardArray : MonoBehaviour {
	public ArrayList MyCardArray=new ArrayList();
	public static CardArray _this;
	public GameObject Temp;
	// Use this for initialization
	void Awake(){
		_this = this;
		AddCardArray ();
	}
	void Start () {
	
	}
	//卡组的增加
	public void AddCardArray(){
		for (int i=0; i<40; i++) {
			MyCardArray.Add((GameObject)Instantiate(Temp,transform.position,Temp.transform.rotation));		
		}

	}
	// Update is called once per frame
	void Update () {
	
	}
}
