using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MapManager : MonoBehaviour {
	public GameObject Card_PosPointer;
	public List<GameObject> MyCardPos=new List<GameObject>();
	public float offset=1.5f;
	public float hight=-5.5f;
	public int[,] map=new int[10,10];
	public static MapManager _this;
	// Use this for initialization
	void Awake(){
		_this = this;
		LoadMap ();
		DrawMap ();
	}
	void Start () {

	}
	//初始化卡位上的卡的信息
	public void LoadCardPos(ArrayList CardArray){
		for (int i=0; i<10; i++) {
						for (int j=0; j<10; j++) {
				if(map[i,j]==2&&MyCardPos[i+j*10].GetComponent<CardPos>().ThisMoveCard==null){
					foreach(GameObject obj in CardArray){
						if(obj!=null){
							obj.transform.position=MyCardPos[i+j*10].transform.position;
							obj.GetComponent<Card>().FindCardPos();
							obj.GetComponent<Card>().MyCardPosion=Card.CardPosition.InPlay;
							CardArray.Remove(obj);
							break;
						}
					}
				}
						}
				}
	}
	//载入卡位的信息
	void LoadMap(){
		for (int i=0; i<10; i++) {
			for(int j=0;j<10;j++){
				map[i,j]=1;
			}		
		}
		map [1, 2] = 2;
		map [1, 1] = 2;
		map [2, 2] = 2;
		map [2, 1] = 2;
		map [1, 7] = 2;
		map [1, 8] = 2;
		map [2, 7] = 2;
		map [2, 8] = 2;


	}
	//实例化卡位
	void DrawMap(){
		for (int i=0; i<10; i++) {
			for(int j=0;j<10;j++){
				if(map[i,j]!=0){
	GameObject temp=(GameObject)Instantiate(Card_PosPointer,new Vector3(i*offset,hight,j*offset),Card_PosPointer.transform.rotation);
					temp.GetComponent<CardPos>().MapX=i;
					temp.GetComponent<CardPos>().MapY=j;
					MyCardPos.Add(temp);
				}
			}		
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
	//攻击距离特效
	public ArrayList ShowDistance(ArrayList list){
		foreach (GameObject obj in list) {
			GameObject TempObj=obj.GetComponent<CardPos>().ThisMoveCard;
			if(TempObj!=null){
			if(TempObj.GetComponent<Card>().MyCardState==Card.CardState.Up){
				obj.GetComponent<SpriteRenderer>().color=new Color(0,1,0,0.5f);
				obj.GetComponent<SpriteRenderer>().sortingLayerName="pointer";
			}else{
				list.Remove(obj);
					break;
				}
			}else{
				obj.GetComponent<SpriteRenderer>().color=new Color(0,1,0,0.5f);
				obj.GetComponent<SpriteRenderer>().sortingLayerName="pointer";
			}

		}
		return list;
	}
	//取消特效
	public void DisDistance(ArrayList list){
		foreach (GameObject obj in list) {
			obj.GetComponent<SpriteRenderer>().color=new Color(1,1,1,1);
			obj.GetComponent<SpriteRenderer>().sortingLayerName="cardpos";
		}
	}
}
