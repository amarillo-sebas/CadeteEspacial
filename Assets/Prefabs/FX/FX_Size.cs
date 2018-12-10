using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_Size : MonoBehaviour {
	public ParticleSystem ps;

	public void SetSize (float scale, float size) {
		var main = ps.main;
		main.startSize = new ParticleSystem.MinMaxCurve(20f * size, 40f * size);
		//main.startSizeMultiplier = scale;
		if (transform.childCount > 0) {
			FX_Size child = transform.GetChild(0).GetComponent<FX_Size>();
			if (child) {
				child.SetSize(scale, size);
			}
		}
	}
}
