using UnityEngine;
using System.Collections;

public class GameManager : SingletonMonoBehaviourFast<GameManager> {

	// Use this for initialization
	void Start () {
	}

	public void GameStart() {
		Application.LoadLevel ("Main");
	}
}
