using UnityEngine;
using System.Collections;
//卡的数据属性
[System.Serializable]
public class property{
	public int life,energy;
	public int attack,defence;
	public int distance;
	public int lvl;
	public int gold,exe;
	public int NowExe,MaxExe;

}
public class Card : MonoBehaviour {
	//卡的所有信息
	public enum CardElement{
		Nothing,
	}
	public enum Race{
		Nothing,
	}
	public enum Type{
		Nothing,
		Prop,
		AmbushProp,
		Unit,
		Hero,
	}
	public enum InPlayAbility{
		Can,
		CanNot,
	}
	public enum MoveAbility{
		Can,
		CanNot,
	}
	public enum CardPosition{
		InPlay,
		InMainDeck,
		InExtraDeck,
		InHand,
	}
	public enum CardState{
		Up,
		Down,
	}
	public enum CardAttackState
   {     Can,
		 CanNot,
		}
	public CardState MyCardState=CardState.Down;
	public MoveAbility MyMove=MoveAbility.CanNot;
	public CardPosition MyCardPosion=CardPosition.InMainDeck;
	public CardAttackState MyCardAttack=CardAttackState.CanNot;
	public InPlayAbility MyPlayAbility=InPlayAbility.CanNot;
	public bool MyCardAttackBlock=true;
	public Type MyType=Type.Nothing;
	public string Name;
	public Sprite TheBack,TheFront;
	public GameObject NowCardPos;
	public float Move_Time=0.2f,Max_Move_Time=0.2f,Rock_Time=0.5f,Max_Rock_Time=0.5f,MyAttackTime=0.2f,Max_Attack_Time=0.2f;
	public float Hight=-5.5f,Attack_Speed=5f;
	public int Attack_Time = 1,Attack_Max_Dis=1,Atack_Min_Dis=0;
	public ArrayList WayPointer=new ArrayList();
	public bool IsMove=false,IsAttack=false;
	int index=0;

	public property MyProperty=new property();
	// Use this for initialization
	void Start () {
		TheFront = this.gameObject.GetComponent<SpriteRenderer> ().sprite;
		//所有的初始化
		CardInPlayAbilityIns ();
		CardPosionIns ();
		CardStateIns ();
		CardMoveIns ();
	}
	void CardTypeIns(){

	}
	void CardInPlayAbilityIns(){
		if (MyType == Type.AmbushProp || MyType == Type.Prop) {
			MyPlayAbility=InPlayAbility.CanNot;		
		}
	}
	void CardAttackIns(){
		if (MyType == Type.Unit || MyType == Type.Hero) {
			MyCardAttack=CardAttackState.Can;
		}
	}
	void CardMoveIns(){
		if (MyType == Type.Unit || MyType == Type.Hero) {
						if (MyCardState == CardState.Down) {
								MyMove = MoveAbility.CanNot;		
						} else if (MyCardState == CardState.Up) {
								MyMove = MoveAbility.Can;
						}
				} else {
			MyMove = MoveAbility.CanNot;
		}
	}
	void CardPosionIns(){
		if (FindCardPos ()) {
			MyCardPosion = CardPosition.InPlay;
		} else {
			MyCardPosion=CardPosition.InMainDeck;
		}
		if (MyCardPosion == CardPosition.InMainDeck) {
			MyCardState = CardState.Down;
		}
	}
	void CardStateIns(){
		if (MyCardState == CardState.Down) {
			this.gameObject.GetComponent<SpriteRenderer>().sprite=TheBack;
			MyCardAttack=CardAttackState.CanNot;
		}
	}
	void SetPos(){

	}
	//翻牌
	public void RockCard(){
		Rock_Time -= Time.deltaTime;
		if (Rock_Time >=0) {
			this.gameObject.transform.Rotate (0, Time.deltaTime * 360, 0);
				} else {
			Rock_Time=Max_Rock_Time;
			this.gameObject.GetComponent<SpriteRenderer>().sprite=TheFront;
			this.gameObject.transform.eulerAngles=new Vector3(90,0,0);
			MyCardState=CardState.Up;
			//DrawCard._this.NowDrawCard.Remove(this.gameObject);
			CardInPlayAbilityIns();
			CardAttackIns();
			CardMoveIns();
			//记得优化手牌
			if(MyPlayAbility==InPlayAbility.CanNot){
				HandCard._this.AddMyHandCard(this.gameObject);
				MyCardPosion=CardPosition.InHand;
				ClearPosInformation();
			}
		}
	}
	void DisCard(){

	}
	//移动
	public void Move(ArrayList MyWayPointer){
		if (index >= MyWayPointer.Count) {
			index=0;
			GameObject temp=(GameObject)WayPointer[WayPointer.Count-1];
			NowCardPos=temp;
			temp.GetComponent<CardPos>().ThisMoveCard=this.gameObject;
			temp=(GameObject)WayPointer[0];
			temp.GetComponent<CardPos>().ThisMoveCard=null;
			CardMove._this.ClearAllPointer();
			WayPointer.Clear();
			IsMove=false;
			return;		
		}
		Move_Time -= Time.deltaTime;
		GameObject TempObj= (GameObject)WayPointer[index];
		if(Move_Time<=0){
			NowCardPos.GetComponent<CardPos>().ThisMoveCard=null;
			this.transform.position=TempObj.transform.position;
			FindCardPos();
			index+=1;
			Move_Time=Max_Move_Time;
		}


	}
	//攻击
	void Attack(GameObject BeAttackObj){
		MyAttackTime -= Time.deltaTime;
		if (MyAttackTime > Max_Attack_Time/2) {
						Vector3 MyPos = transform.position;
						Vector3 AimPos = BeAttackObj.transform.position;
						transform.position = Vector3.Lerp (MyPos, AimPos, Time.deltaTime * Attack_Speed);    
						/* float x=transform.position.x;
			float z=transform.position.z;
			float Objx=BeAttackObj.transform.position.x;
			float Objz=BeAttackObj.transform.position.z;
			float MyX= Mathf.Clamp(Time.deltaTime*50f,x,Objx);
			float MyY=Mathf.Clamp(Time.deltaTime*50f,z,Objz);
			transform.position=new Vector3(MyX,Hight,MyY);*/

				} else if (MyAttackTime <= Max_Attack_Time/2 && MyAttackTime >= 0) {
			Vector3 MyPos = transform.position;
			Vector3 AimPos = NowCardPos.transform.position;
			transform.position = Vector3.Lerp (MyPos, AimPos, Time.deltaTime * Attack_Speed); 
				} else {

			transform.position= NowCardPos.transform.position;
			MyAttackTime = Max_Attack_Time;
			IsAttack = false;
			Attack_Time -= 1;
		}
	}
	// Update is called once per frame
	void Update () {
		if (MyCardState == CardState.Down) {
			IsMove=false;		
		}
		if (IsMove) {
						Move (WayPointer);
				}
		if (IsAttack) {
			Attack(CardAttack._this.BeAttackPos);		
		}
	}
	//寻找自己的对应的卡牌位置
	public bool FindCardPos(){
		GameObject[] AllCardPos=GameObject.FindGameObjectsWithTag("CardPos");
		for (int i=0; i<AllCardPos.Length; i++) {
			//print(AllCardPos[i].transform.position);
			if(Vector3.Distance(AllCardPos[i].transform.position,this.transform.position)==0){
				NowCardPos=AllCardPos[i];
				AllCardPos[i].GetComponent<CardPos>().ThisMoveCard=this.gameObject;
				return true;
			}		
		}
		return false;
	}
	void ClearPosInformation(){
		NowCardPos.GetComponent<CardPos> ().ThisMoveCard = null;
		NowCardPos = null;
	}
	void MyCardLayer(){
		if (MyCardPosion == CardPosition.InPlay) {
			this.gameObject.layer=9;
		}
		if (MyCardPosion == CardPosition.InHand) {
			this.gameObject.layer=10;		
		}
		if (MyCardPosion == CardPosition.InMainDeck) {
			this.gameObject.layer=11;		
		}
	}
	void LateUpdate(){
		MyCardLayer ();
	}

}
