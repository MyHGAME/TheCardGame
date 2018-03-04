using UnityEngine;
using System.Collections;
//卡的行为：定义卡的所有行为动画，例如，翻牌，卡组加牌，使用卡等
//卡行为接口
public class CardBehaviorInfor {

	public void CardBehaviorInforSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	public IEnumerator CardBehaviorInforWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
