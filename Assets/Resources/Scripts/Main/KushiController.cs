using UnityEngine;
using System.Collections;
using UniRx;

public class KushiController : MonoBehaviour {
	public GameObject material1;
	public GameObject material2;
	public GameObject material3;
	public GameObject tableObject;
	
	private float yakiStartTime;
	private YakiState state = YakiState.raw;
	private bool completed = false;

	private Sprite spriteNama;
	private Sprite spriteChoiyake;
	private Sprite spriteKongari;
	private Sprite spriteKoge;

	private enum YakiState {
		raw = 0, // namaniku 0.0f - 1.0f      // 0pt
		rare, // namayakeniku 1.0f-2.8f   // 10pt
		midium, // kongari 2.8f - 3.2f    // 100pt
		burnt, // 3.2f-                   // 5pt
	}

	void Start() {
	}

	public void SetCount (int count) {
		material1.SetActive (false);
		material2.SetActive (false);
		material3.SetActive (false);

		if (count >= 1) {
			material1.SetActive (true);
		}

		if (count >= 2) {
			material2.SetActive (true);
		}

		if (count >= 3) {
			material3.SetActive (true);
		}

		spriteNama = Resources.Load ("Sprites/material_negi_nama", typeof(Sprite)) as Sprite;
		spriteChoiyake = Resources.Load ("Sprites/material_negi_choiyake", typeof(Sprite)) as Sprite;
		spriteKongari = Resources.Load ("Sprites/material_negi_kongari", typeof(Sprite)) as Sprite;
		spriteKoge = Resources.Load ("Sprites/material_negi_koge", typeof(Sprite)) as Sprite;
	}

	public void StartYaki () {
		YakiController.Instance.AddKushi (this.gameObject);
		yakiStartTime = Time.time;

		// 1sec yaki state change. 
		float tick = 0;
		float generateTimeSpan = 0.1f;
		Observable.Timer(System.TimeSpan.FromSeconds(generateTimeSpan))
			.RepeatUntilDestroy (this.gameObject)
				.Subscribe (x => {
					tick = Time.time - yakiStartTime;

					if (completed) {
						return;
					}

					if (tick >= 3.5f) {
						FailureYaki ();
					}
					else if (tick >= 3.2f) {
						SetState (YakiState.burnt);
					}
					else if (tick >= 2.8f) {
						SetState (YakiState.midium);
					}
					else if (tick >= 1.0f) {
						SetState (YakiState.rare);
					}
				});
	}

	void SetState(YakiState nextState) {
		state = nextState;
		Sprite stateSprite = null;

		switch (state) {
		case YakiState.raw:
			stateSprite = spriteNama;
			break;
		case YakiState.rare:
			stateSprite = spriteChoiyake;
			break;
		case YakiState.midium:
			stateSprite = spriteKongari;
			break;
		case YakiState.burnt:
			stateSprite = spriteKoge;
			break;
		}

		material1.GetComponent<SpriteRenderer> ().sprite = stateSprite;
		material2.GetComponent<SpriteRenderer> ().sprite = stateSprite;
		material3.GetComponent<SpriteRenderer> ().sprite = stateSprite;
	}

	public void CompleteYaki () {
		completed = true;

		switch (state) {
		case YakiState.raw:
			break;
		case YakiState.rare:
			GameManager.Instance.UpdateScore(10.0f);
			break;
		case YakiState.midium:
			GameManager.Instance.UpdateScore(100.0f);
			break;
		case YakiState.burnt:
			GameManager.Instance.UpdateScore(5.0f);
			break;
		}

		Vector3 tablePosition = tableObject.transform.position;
		this.gameObject.transform.position = new Vector3 (tablePosition.x, tablePosition.y+5, tablePosition.z);
	}

	public void FailureYaki () {
		completed = true;
		YakiController.Instance.RemoveKushi (this.gameObject);

		Destroy (this.gameObject);
	}
}
