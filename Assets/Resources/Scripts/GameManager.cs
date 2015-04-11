using UnityEngine;
using System.Collections;

public class GameManager : SingletonMonoBehaviourFast<GameManager> {
	private CookState state;
	private enum CookState {
		nothing,
		cutting,
		burning,
	};

	// Use this for initialization
	void Start () {
		state = CookState.nothing;
	}

	public void startCutting () {
		state = CookState.cutting;
	}

	public bool isCutting () {
		return state == CookState.cutting;
	}

	public bool isBuring () {
		return state == CookState.burning;
	}
}
