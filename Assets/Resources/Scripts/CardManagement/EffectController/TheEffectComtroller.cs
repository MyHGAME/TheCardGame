using UnityEngine;
using System.Collections;
//卡的生成原理：添加卡类，卡类包括卡的各种信息，卡的效果生成，效果的配置
public class TheEffectComtroller {

	public void TheEffectComtrollerSimplePasses() {
		// Use the Assert class to test conditions.
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	public IEnumerator TheEffectComtrollerWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}
