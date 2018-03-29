using UnityEngine;
using System.Collections;

public class DrawCard : MonoBehaviour {
	public int MyDrawCardTimes=1,NowDrawCardNum=0;
	public ArrayList NowDrawCard=new ArrayList();
	public static DrawCard _this;
	// Use this for initialization
	void Start () {
		_this = this;
	}
	
	// Update is called once per frame
	void Update () {
		DrawDownCard ();
		RockingCard ();
	}
	//判断翻牌
	void DrawDownCard(){
		GameObject temp = Choice._this.NowCard;
		if (CardMove._this.WayPointer.Count==0&&temp!= null&&MyDrawCardTimes>0) {
			if(temp.GetComponent<Card>().MyCardPosion==Card.CardPosition.InPlay&&temp.GetComponent<Card>().MyCardState==Card.CardState.Down){
				//Choice._this.NowCard.GetComponent<Card>().RockCard();
				if(!NowDrawCard.Contains(Choice._this.NowCard)){
					MyDrawCardTimes-=1;
					NowDrawCard.Add(Choice._this.NowCard);
				}
			}		
		}

	}
	//支持连续翻牌功能
	void RockingCard(){
		if (NowDrawCard != null) {
			foreach(GameObject obj in NowDrawCard){
				if(obj!=null){
					if(obj.GetComponent<Card>().MyCardState==Card.CardState.Down){
					obj.GetComponent<Card>().RockCard();
					}else{
						NowDrawCard.Remove(obj);
						break;
					}
				}
			}
		}
	}
}
