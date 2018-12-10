using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBorderControl : MonoBehaviour {
	public Colorful.FastVignette borderFX;
	public MessageDisplayer messages;

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "LevelBorder") {
			if (!_counting) {
				if (borderFX != null) borderFX.Darkness = 50f;
				messages.Message("OE TE TÁS YENDO\nMUY LEJOS PES\nCADETE REGRESA", 2f);
			}
			
		}
	}
	void OnCollisionExit(Collision c) {
		if (c.gameObject.tag == "LevelBorder") {
			if (borderFX != null) borderFX.Darkness = 0f;
			StartCoroutine(ClearMessage(2f));
		}
	}

	private bool _counting;
	IEnumerator ClearMessage (float t) {
		_counting = true;
		messages.ClearMessage();
		yield return new WaitForSeconds(t);
		_counting = false;
	}

	public void PlayerGetsIn (Transform p) {
		borderFX = p.GetComponentInChildren<Colorful.FastVignette>();
	}
	public void PlayerGetsOut () {
		borderFX = null;
	}
}
