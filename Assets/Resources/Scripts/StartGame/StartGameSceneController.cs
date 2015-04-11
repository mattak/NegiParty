using UnityEngine;
using System.Collections;

public class StartGameSceneController : MonoBehaviour {
	public void OnClickStartButton() {
		Application.LoadLevel ("Main");
	}
}
