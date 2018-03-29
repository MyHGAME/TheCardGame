using UnityEngine;
using System.Collections;

public class HandCard : MonoBehaviour {
	public ArrayList HandList=new ArrayList();
	public ArrayList ShowMyCard=new ArrayList();
	float OffSetX=1.35f;
	float OffSet=0.35f;
	float MyPosX=7f;
	public float ScaleX=1;
	public float OffSetY=0.1f;
	public static HandCard _this;
	public GameObject HandCardPos;
	public float MyAngle=60;
	public LayerMask HandCardLayer;
	public float ScaleTimes=1.5f;
	public float MaxHandCardX=8.82f,MaxDisHandCard=4.42f;
	// Use this for initialization
	void Start () {
		_this = this;
	}
	// Update is called once per frame
	void Update () {
		RayHit ();
	}
	public void AddMyHandCard(GameObject Temp){
				HandList.Add (Temp);
				Temp.transform.position = HandCardPos.transform.position;
				Temp.transform.eulerAngles = new Vector3 (MyAngle, 0, 0);
				//Temp.transform.parent = HandCardPos.transform;
				//float x = HandCardPos.transform.position.x;
				float y = HandCardPos.transform.position.y;
				float z = HandCardPos.transform.position.z;
				
				float Scale = ScaleX * HandList.Count;
				if (HandList.Count > 0) {
						GameObject temp = (GameObject)HandList [0];
						temp.transform.position = new Vector3 (MyPosX + (OffSet - ScaleX) * HandList.Count, y, z);
				}
				if (HandList.Count < 8) {
			int n = 0;
						foreach (GameObject temp in HandList) {
								//temp.layer=HandCardPos.layer;
								//int Index=HandList.Count-n;
								if (n != 0) {
										GameObject FristCard = (GameObject)HandList [0];
										temp.transform.position = new Vector3 (FristCard.transform.position.x - n * (OffSetX - Scale), y, z);
								}
								n += 1; 
								temp.GetComponent<SpriteRenderer> ().sortingOrder = n;
					}
				} else {
			int n = 0;
			GameObject NewObj=(GameObject)HandList[HandList.Count-2];
			GameObject OldObj=(GameObject)HandList[0];
			Vector3 pos=NewObj.transform.position;
			Vector3 pos2=OldObj.transform.position;
			//print(pos+"   "+pos2);
			float Dis=Vector3.Distance(pos,pos2);
			float OffSetDis=MaxDisHandCard/HandList.Count;
			//print(OffSetDis);
			foreach(GameObject temp in HandList){
				temp.transform.position=new Vector3(MaxHandCardX-n*OffSetDis,pos.y,pos.z);
				n += 1;
				temp.GetComponent<SpriteRenderer> ().sortingOrder = n;
			}		
		}
		}
	void ArrayTheCard(){

	}
	void ShowTheCard(GameObject result){
		if (!ShowMyCard.Contains (result)) {
			ShowMyCard.Clear();
			ShowMyCard.Add (result);
		}
		ClearShowTheCard ();
		foreach(GameObject obj in ShowMyCard){
			//obj.GetComponent<SpriteRenderer>().sortingOrder=HandList.Count+1;
			Vector3 pos=obj.transform.position;
			obj.transform.position=new Vector3(pos.x,-0.856f,-1.6f);
		}
	}
	void ClearShowTheCard(){
		int n = 0;
		foreach (GameObject obj in HandList) {
			                    obj.transform.position=new Vector3(obj.transform.position.x,-1f,-1.85f);
								obj.GetComponent<SpriteRenderer> ().sortingOrder = n;
								n += 1;
				}
	}
	void RayHit(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100, HandCardLayer)) {
						if (hit.collider != null) {
								if (hit.collider.tag == "Card") {
					if(Input.GetMouseButtonDown(0)){					
						ShowTheCard (hit.collider.gameObject);
					}
								}
						} 
				} 
	}
    
}
