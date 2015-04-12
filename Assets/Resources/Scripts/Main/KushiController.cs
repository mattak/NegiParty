using UnityEngine;
using System.Collections;
using UniRx;

public class KushiController : MonoBehaviour {
	public GameObject material1;
	public GameObject material2;
	public GameObject material3;
	private float yakiStartTime;
	private YakiState state = YakiState.raw;

	private enum YakiState {
		raw = 0, // namaniku 0.0f - 1.0f      // 0pt
		rare, // namayakeniku 1.0f-2.8f   // 10pt
		midium, // kongari 2.8f - 3.2f    // 100pt
		burnt, // 3.2f-                   // 5pt
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
	}

	public void StartYaki () {
		yakiStartTime = Time.time;

		// 1sec yaki state change. 
		float tick = 0;
		float generateTimeSpan = 0.1f;
		Observable.Timer(System.TimeSpan.FromSeconds(generateTimeSpan))
			.RepeatUntilDestroy (this.gameObject)
				.Subscribe (x => {
					tick += generateTimeSpan;

					if (tick >= 3.5f) {
						EndYaki ();
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

		string imagePath = "";

		switch (state) {
		case YakiState.raw:
			imagePath = "Sprites/material_negi_nama";
			break;
		case YakiState.rare:
			imagePath = "Sprites/material_negi_choiyake";
			break;
		case YakiState.midium:
			imagePath = "Sprites/material_negi_kongari";
			break;
		case YakiState.burnt:
			imagePath = "Sprites/material_negi_koge";
			break;
		}

		material1.GetComponent<SpriteRenderer>().sprite = Resources.Load (imagePath, typeof(Sprite)) as Sprite;
		material2.GetComponent<SpriteRenderer>().sprite = Resources.Load (imagePath, typeof(Sprite)) as Sprite;
		material3.GetComponent<SpriteRenderer>().sprite = Resources.Load (imagePath, typeof(Sprite)) as Sprite;
	}

	public void EndYaki () {
		Debug.Log ("endyaki: " + state);
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

		Destroy (this.gameObject);
	}
}
