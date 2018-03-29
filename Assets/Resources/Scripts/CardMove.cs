using UnityEngine;
using System.Collections;
public class CardMove : MonoBehaviour {
	//路点，传递给卡牌然后移动
	public ArrayList WayPointer=new ArrayList();
	//实例化的圆点，方便观察
	ArrayList CreatPointer = new ArrayList ();
	//错误的路点提示
	ArrayList WorryPointer=new ArrayList();
	public LayerMask CardPos;
	public GameObject Pointer;
	//是否有路点
	public bool HavePointer=false,MouseDown=false,IsMoveState=false;
	public static CardMove _this;
	// Use this for initialization
	void Start () {
		_this = this;
	}
	
	// Update is called once per frame
	void Update () {
		IsMoveState=JudgeMoveState ();
		if (!CardAttack._this.Attack) {
						RayHit ();
				}
	
	}
	//检测射线
	void RayHit(){
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 100, CardPos)) {
						if (hit.collider == null)
								return;
						if (hit.collider.tag == "CardPos") {
								//检测是否为可以移动的卡
						if (Choice._this.NowCard != null) {
					    if(Choice._this.NowCard.GetComponent<Card>().MyMove==Card.MoveAbility.Can) {
							CreateWayPointer(hit,Pointer);
					                                              }
					   
								}
				else{
					CreateTheDistance(hit,Pointer);
				}
						}
				}
		}
	bool JudgeMoveState(){
		GameObject[] TempObj=GameObject.FindGameObjectsWithTag("Card");
		for (int i=0; i<TempObj.Length; i++) {
			if(TempObj[i].GetComponent<Card>().IsMove){
				return true;
			}
		}
		return false;
	}
	//产生路点，点击了卡牌
	void CreateWayPointer(RaycastHit hit,GameObject TempObj){
		if (!HavePointer&&!IsMoveState) {
			if (Input.GetMouseButton (0)) {
								if (!WayPointer.Contains (hit.collider.gameObject)) {
					MainCreater(hit,TempObj);	
				              }
						} 
			else if (Input.GetMouseButtonUp (0)) {
				if(WayPointer.Count>0){
				if(WayPointer[0]==Choice._this.NowCard.GetComponent<Card>().NowCardPos){
					HavePointer = true;		
				}else{
					ClearAllPointer();
					}
				}
				//HavePointer = true;
				if(WayPointer.Count==1){
					ClearAllPointer();
				}
						}

				}

		if (WayPointer.Count!=0&&HavePointer) {
			if(Input.GetMouseButtonDown(0)){
				MouseDown=true;
			}		
		}
		if (MouseDown) {
			if(Input.GetMouseButtonUp(0)){
			if(hit.collider.gameObject==(GameObject)WayPointer [WayPointer.Count - 1]){
				if(WayPointer.Count>1){
					//move!
					
					Choice._this.NowCard.GetComponent<Card>().WayPointer=WayPointer;
					
					Choice._this.NowCard.GetComponent<Card>().IsMove=true;
					//WayPointer.Clear();
				}
				HavePointer=false;
			}else{
				ClearAllPointer();
				HavePointer=false;
				}

				MouseDown=false;
			}

		}
	}
	//产生距离路点，没有点击卡牌
	void CreateTheDistance(RaycastHit hit,GameObject TempObj){
		
		if (Input.GetMouseButton (0)) {
			if(!WayPointer.Contains(hit.collider.gameObject)){
				MainCreater(hit,TempObj);
			}
		}else if (Input.GetMouseButtonUp (0)) {
			ClearAllPointer();
		}
	}
	//产生错误路点
	void CreateWorryPointer(GameObject TempObj,Vector3 pos){
		foreach (GameObject obj in WorryPointer) {
		if(obj.gameObject.transform.position==pos)
				return;
		}
		GameObject worrypointer = (GameObject)Instantiate (TempObj, pos, TempObj.transform.rotation);
		worrypointer.GetComponent<SpriteRenderer> ().color = new Color (1, 0, 0, 1);
		WorryPointer.Add (worrypointer);

	}
	//判断该路点是否正确
	int JudgeResult(RaycastHit hit){
		int Result = 0;
		if (WayPointer.Count >= 1) {
						int mapx1 = hit.collider.GetComponent<CardPos> ().MapX;
						int mapy1 = hit.collider.GetComponent<CardPos> ().MapY;
						GameObject temp = (GameObject)WayPointer [WayPointer.Count - 1];
						int mapx2 = temp.GetComponent<CardPos> ().MapX;
						int mapy2 = temp.GetComponent<CardPos> ().MapY;
					    Result = Mathf.Abs (mapx1 - mapx2) + Mathf.Abs (mapy1 - mapy2);
					if (Choice._this.NowCard!=null&&hit.collider.gameObject.GetComponent<CardPos> ().ThisMoveCard != null) {
												Result = 2;
						}
				}
		return Result;
	}
	//实例化路点
	void MainCreater(RaycastHit hit,GameObject TempObj){
		if(JudgeResult(hit)<=1){
			ClearObj(WorryPointer);
			WayPointer.Add(hit.collider.gameObject);
			CreatPointer.Add((GameObject)Instantiate(TempObj,hit.collider.transform.position,TempObj.transform.rotation));
		}else{

			CreateWorryPointer(TempObj,hit.collider.transform.position);
		}
	}
	//清除所有路点
	public void ClearAllPointer(){
		WayPointer.Clear();
		ClearObj(CreatPointer);
		ClearObj(WorryPointer);
	}
	//清除特定路点
	void ClearObj(ArrayList temp){
		foreach(GameObject obj in temp){
			if(obj!=null){
				Destroy(obj);
			}
		}
		temp.Clear();
	}
}
