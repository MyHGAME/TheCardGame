using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//卡的类型，空，陷阱卡，技能卡，单位卡，英雄卡（应该将单位卡与英雄卡归类）
public enum Type{
	None,
	Prop,
	Skill,
	Unit,
	Hero,
}
//卡的位置,在场上，在主卡组，在额外卡组，在手牌，在弃牌区（需要继续细分，分对方与我方的位置？）
public enum CardLocation{
	InPlay,
	InMainDeck,
	InExtraDeck,
	InHand,
	InAbandonedArea,
}
//卡的翻转状态
public enum CardFlipState{
	Up,
	Down,
}
//
//卡的基类,包含卡的基本功能--翻牌，加入手牌，送进弃牌区，使用手牌，
//包含效果功能，使用效果功能，判断使用效果条件，出场条件功能,卡的时点，卡位关联，关于属性的功能，ui关联
//单位卡类，包含单位卡的基本功能--攻击，移动，
//技能卡类,技能卡类型：辅助型，普通型，反击型，装备型，辅助型：翻开就出场，条件不满足失去效力，永久在场
//其他类型：翻开就返回手牌，使用占用使用卡位 普通型：只能在自己回合使用 反击型：满足条件可以使用，对方回合也可以 装备型：对单位使用
//陷阱卡类：翻开返回手牌，根据卡的效果埋伏在场上，有符合条件的单位经过卡位则触发效果,一个卡位可埋伏多张陷阱卡，触发条件由埋伏先后决定
//卡的基本信息：卡的颜色决定卡的类型，在卡的右上角有卡的类型描述，卡的上方是卡名称，卡的中间区域是卡的图片，卡的下方区域是卡的效果和属性
//单位卡：有生命值，能量值，经验值（英雄卡），等级，攻击力，防御力，行动力（速度）
//技能卡、陷阱卡：名字的右边是技能卡的类型图标，在效果的上方说明卡的类型，
public class CardClass : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
