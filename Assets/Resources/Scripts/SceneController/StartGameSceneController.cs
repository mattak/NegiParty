using UnityEngine;
using System.Collections;

public class StartGameSceneController : MonoBehaviour {
	public void OnClickStartButton() {
		Debug.Log ("onClick");
		GameManager.Instance.GameStart ();
	}
}
