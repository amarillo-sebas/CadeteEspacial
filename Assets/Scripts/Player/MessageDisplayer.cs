using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageDisplayer : MonoBehaviour {
	public TextMeshProUGUI txtDisplay;

	private string _messageToType;

	public float typingInterval;
	public float messageTime;

	private bool _typing = false;
	public List<string> _messageQueue = new List<string>();

	void OnEnable () {
		_messageQueue.Clear();
		txtDisplay.text = "";
		_typing = false;
	}

	public void Message (string m) {
		txtDisplay.text = m;
		EndMessage();
	}
	public void Message (string m, float t) {
		txtDisplay.text = m;
		EndMessage(t);
	}

	public void TypeMessage (string m) {
		if (!_typing) {
			_typing = true;
			_messageToType = m;
			StartCoroutine(TypeText());
		} else {
			_messageQueue.Add(m);
		}
			
	}
	public void TypeMessage (string m, float t) {
		if (!_typing) {
			_typing = true;
			_messageToType = m;
			StartCoroutine(TypeText(t));
		} else {
			_messageQueue.Add(m);
		}
			
	}

	public void ClearMessage () {
		txtDisplay.text = "";
	}

	IEnumerator TypeText () {
		foreach (char letter in _messageToType.ToCharArray()) {
			txtDisplay.text += letter;
			yield return new WaitForSeconds (typingInterval);
		}
		StartCoroutine(EndMessage());
	}
	IEnumerator EndMessage () {
		if (messageTime != 0f) {
			yield return new WaitForSeconds (messageTime);
			txtDisplay.text = "";
			if (_messageQueue.Count > 0) {
				_messageToType = _messageQueue[_messageQueue.Count - 1];
				_messageQueue.RemoveAt(_messageQueue.Count - 1);
				StartCoroutine(TypeText());
			} else {
				_typing = false;
			}
		}
	}

	IEnumerator TypeText (float t) {
		foreach (char letter in _messageToType.ToCharArray()) {
			txtDisplay.text += letter;
			yield return new WaitForSeconds (typingInterval);
		}
		StartCoroutine(EndMessage(t));
	}
	IEnumerator EndMessage (float t) {
		yield return new WaitForSeconds (t);
		txtDisplay.text = "";
		if (_messageQueue.Count > 0) {
			_messageToType = _messageQueue[_messageQueue.Count - 1];
			_messageQueue.RemoveAt(_messageQueue.Count - 1);
			StartCoroutine(TypeText());
		} else {
			_typing = false;
		}
	}
}
