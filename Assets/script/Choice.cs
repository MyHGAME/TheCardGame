using UnityEngine;
using System.Collections;

public class Choice : MonoBehaviour {
	//选中的卡
	public GameObject NowCard;
	public LayerMask TheCard,ThePos;

	public static Choice _this;
	// Use this for initialization
	void Start () {
		_this = this;
	}
	void RayHit(){
		//射线判断
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100, TheCard)) {
						if (Input.GetMouseButtonDown (0)) {
				if(CardMove._this.WayPointer.Count==0&&!CardAttack._this.Attack){
								if (NowCard != null) {
						//增加判断当翻牌次数没有就不能点击没翻的牌
										if (!NowCard.GetComponent<Card> ().IsMove&&!NowCard.GetComponent<Card>().IsAttack) {
												NowCard =ChoiceCard (hit, NowCard);                    
												CardMove._this.HavePointer = false;
										}
								} else {
										NowCard = ChoiceCard (hit, NowCard);
								}
				}
			}
				}
	}
	//返回当前选中的卡
	GameObject ChoiceCard(RaycastHit TempHit,GameObject TempObj){
		if (TempHit.collider.tag == "Card") {
				TempObj=TempHit.collider.gameObject;
			}
		return TempObj;
		}


	// Update is called once per frame
	void Update () {
		RayHit ();
	}

}
