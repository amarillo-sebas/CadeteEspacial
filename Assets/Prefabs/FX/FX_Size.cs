using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_Size : MonoBehaviour {
	public ParticleSystem ps;

	public void SetSize (float scale, float size) {
		var main = ps.main;
		main.startSize = new ParticleSystem.MinMaxCurve(20f * size, 40f * size);
		//main.startSizeMultiplier = scale;
	}
}
