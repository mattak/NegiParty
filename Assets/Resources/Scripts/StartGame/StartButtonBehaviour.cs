using UnityEngine;
using System.Collections;

public class StartButtonBehaviour : MonoBehaviour {
	public void OnClickStartButton() {
		Application.LoadLevel ("Main");
	}
}
