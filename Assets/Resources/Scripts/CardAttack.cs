using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CardAttack : MonoBehaviour {
	//判断状态
	public bool Attack=false,HaveAttackDis=false,InputMouseDown=false;
	public static CardAttack _this;
	//卡牌位置
	public LayerMask MyCardPos;
	//被攻击的目标
	public GameObject BeAttackPos;
	//可以攻击的目标
	ArrayList AttackDis=new ArrayList();
	// Use this for initialization
	void Start () {
		_this = this;
	}
	
	// Update is called once per frame
	void Update () {
		//判断是否按下键
		Attack = InputAttackKey(Attack);
		if (!Attack) {
			//如果没有进入攻击判断，攻击距离就归零
			HaveAttackDis=false;
			if(AttackDis.Count>0){
				//取消攻击距离的特效
				MapManager._this.DisDistance(AttackDis);
			}
		}
		if (Choice._this.NowCard != null) {
			//攻击次数少于0，不能攻击
						if (Choice._this.NowCard.GetComponent<Card> ().Attack_Time <= 0) {
								Attack = false;
						}
				}
		JudgeAttack (Attack);
		RayHit (Attack);
	}
	bool InputAttackKey(bool attack){
		if (Input.GetKey (KeyCode.A)) {
			attack = true;
		} else {
			attack=false;		
		}
		return attack;
	}
	void RayHit(bool attack){
		//射线判断攻击目标
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100, MyCardPos)) {
			if(hit.collider!=null){
				if(hit.collider.tag=="CardPos"){
			if(attack){
				if(Input.GetMouseButtonDown(0)){
					InputMouseDown=true;
				}
				if(InputMouseDown){
					if(Input.GetMouseButtonUp(0)){
								//选定目标
						if(AttackDis.Contains(hit.collider.gameObject)){
							BeAttackPos=hit.collider.gameObject;
							Choice._this.NowCard.GetComponent<Card>().IsAttack=true;
						}
						InputMouseDown=false;
					}
				  }
			   }
			}
		}
	}
}
	void JudgeAttack(bool attack){
		//判断能不能进入攻击
		GameObject TempObj = Choice._this.NowCard;
		if ( TempObj!= null) {
			if(TempObj.GetComponent<Card>().MyCardAttack==Card.CardAttackState.Can&&!TempObj.GetComponent<Card>().IsAttack){
				if(TempObj.GetComponent<Card>().Attack_Time>0&&!TempObj.GetComponent<Card>().IsMove){
					if(attack){
						int MaxDis=TempObj.GetComponent<Card>().Attack_Max_Dis;
						int MinDis=TempObj.GetComponent<Card>().Atack_Min_Dis;
						bool Block=TempObj.GetComponent<Card>().MyCardAttackBlock;
						GameObject TempPos=TempObj.GetComponent<Card>().NowCardPos;
						int x=TempPos.GetComponent<CardPos>().MapX;
						int y=TempPos.GetComponent<CardPos>().MapY;
						ArrayList AttackList=new ArrayList();
						if(!HaveAttackDis){
							BeAttackPos=null;
							AttackList=FindDistance(x,y,MinDis,MaxDis,AttackList,Block);
							HaveAttackDis=true;
						}
						if(AttackList.Count>0){
							AttackDis=AttackList;
						}
						//攻击距离特效
							AttackDis=MapManager._this.ShowDistance(AttackDis);
						
					}
				}
			}		
		}
		if (AttackDis.Count > 0) {
			//正在攻击的时候取消攻击距离特效
						if (TempObj.GetComponent<Card> ().IsAttack) {
								MapManager._this.DisDistance (AttackDis);
						}
				}
	}
	public ArrayList FindDistance(int X,int Y, int MinDistance,int MaxDistance,ArrayList AllDistance,bool Block){
		while(MaxDistance>MinDistance){
			//两种方法，一种是直接找，另一种是循环
			//添加攻击阻挡功能
			foreach(GameObject obj in MapManager._this.MyCardPos){
				int AbsX=Mathf.Abs(obj.GetComponent<CardPos>().MapX-X);
				int AbsY=Mathf.Abs(obj.GetComponent<CardPos>().MapY-Y);
				int Result=AbsX+AbsY;
				if(Result==MaxDistance){
					AllDistance.Add(obj);
				}
			}
			MaxDistance-=1;
		}
		ArrayList list=new ArrayList();
		if (Block) {
			GameObject TempObj=Choice._this.NowCard.GetComponent<Card>().NowCardPos;
			int NowX=TempObj.GetComponent<CardPos>().MapX;
			int NowY=TempObj.GetComponent<CardPos>().MapY;
			foreach (GameObject obj in AllDistance) {
				if(obj.GetComponent<CardPos>().ThisMoveCard!=null){
					ArrayList TempList=new ArrayList();
					int x=obj.GetComponent<CardPos>().MapX;
					int y=obj.GetComponent<CardPos>().MapY;
					TempList=Seach_Block_Obj(list,AllDistance,x,y,NowX,NowY);
					foreach(GameObject temp in TempList){
						if(!list.Contains(temp)){
							list.Add(temp);
						}
					}
				/*	if(x>TempObj.GetComponent<CardPos>().MapX){
						if(y>TempObj.GetComponent<CardPos>().MapX){
							foreach (GameObject obj2 in TempList) {
								int tempx=obj2.GetComponent<CardPos>().MapX;
								int tempy=obj2.GetComponent<CardPos>().MapY;
								if(tempx==x){
									if(tempy>y){
										list.Add(obj2);
									}
								}
							}
						}else if(y<TempObj.GetComponent<CardPos>().MapX){
							foreach (GameObject obj2 in TempList) {
								int tempx=obj2.GetComponent<CardPos>().MapX;
								int tempy=obj2.GetComponent<CardPos>().MapY;
								if(tempx==x){
									if(tempy<y){
										list.Add(obj2);
									}
								}
							}
						}
					else{
							foreach (GameObject obj2 in TempList) {
							int tempx=obj2.GetComponent<CardPos>().MapX;
							int tempy=obj2.GetComponent<CardPos>().MapY;
							if(tempy==y){
								if(tempx>x){
									list.Add(obj2);
								}
							}
							}
						}
			}else if(x<TempObj.GetComponent<CardPos>().MapX){
						if(y>TempObj.GetComponent<CardPos>().MapX){
							foreach (GameObject obj2 in TempList) {
								int tempx=obj2.GetComponent<CardPos>().MapX;
								int tempy=obj2.GetComponent<CardPos>().MapY;
								if(tempx==x){
									if(tempy>y){
										list.Add(obj2);
									}
								}
							}
						}else if(y<TempObj.GetComponent<CardPos>().MapX){
							foreach (GameObject obj2 in TempList) {
								int tempx=obj2.GetComponent<CardPos>().MapX;
								int tempy=obj2.GetComponent<CardPos>().MapY;
								if(tempx==x){
									if(tempy<y){
										list.Add(obj2);
									}
								}
							}
						}
						else {foreach (GameObject obj2 in TempList) {
							int tempx=obj2.GetComponent<CardPos>().MapX;
							int tempy=obj2.GetComponent<CardPos>().MapY;
							if(tempy==y){
								if(tempx<x){
									list.Add(obj2);
								}
							}
							}
						}
					}else{
						if(y>TempObj.GetComponent<CardPos>().MapX){
							foreach (GameObject obj2 in TempList) {
								int tempx=obj2.GetComponent<CardPos>().MapX;
								int tempy=obj2.GetComponent<CardPos>().MapY;
								if(tempx==x){
									if(tempy>y){
										list.Add(obj2);
									}
								}
							}
						}else if(y<TempObj.GetComponent<CardPos>().MapX){
							foreach (GameObject obj2 in TempList) {
								int tempx=obj2.GetComponent<CardPos>().MapX;
								int tempy=obj2.GetComponent<CardPos>().MapY;
								if(tempx==x){
									if(tempy<y){
										list.Add(obj2);
									}
								}
							}
						}
					}*/

				}
				}	
			}
		if(list.Count>0){

			foreach(GameObject obj in list){
				AllDistance.Remove(obj);
			}
		}
		//AllDistance=list;
		return AllDistance;
	}
	ArrayList Seach_Block_Obj(ArrayList list,ArrayList All,int x,int y,int NowX,int NowY){
		int DisX=x-NowX;
		int DisY=y-NowY;
		if (DisX != 0) {
						DisX = DisX / Mathf.Abs (DisX);
				}
		if (DisY != 0) {
			DisY = DisY / Mathf.Abs (DisY);		
		}
		foreach (GameObject obj in All) {
			if((obj.GetComponent<CardPos>().MapX-x)==DisX){
				if((obj.GetComponent<CardPos>().MapY-y)==DisY){
					list.Add(obj);
					NowX=x;
					NowY=y;
					x=obj.GetComponent<CardPos>().MapX;
					y=obj.GetComponent<CardPos>().MapY;

					list=Seach_Block_Obj(list,All,x,y,NowX,NowY);
				}
			}
		}
		return list;
	}
}
