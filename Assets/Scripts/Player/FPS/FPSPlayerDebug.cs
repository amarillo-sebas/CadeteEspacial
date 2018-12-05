using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSPlayerDebug : MonoBehaviour {
	public TextMeshProUGUI txtDisplay;

	public void Log <T>(T param) {
		txtDisplay.text = param + "";
	}
}
