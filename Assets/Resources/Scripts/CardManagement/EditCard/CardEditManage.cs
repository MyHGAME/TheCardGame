using UnityEngine;
using UnityEditor;
using System.Collections;
//卡片编辑类，匹配卡图，卡效果等，可以通过外部导入卡，从这里保存卡，将卡的信息反映到游戏内，游戏能生成相应的卡,能在程序运行时生成卡，也可以在编辑过程生成卡
public class CardEditManage {


	public void CardEditManageSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	public IEnumerator CardEditManageWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
