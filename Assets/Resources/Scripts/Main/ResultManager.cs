using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultManager : SingletonMonoBehaviourFast<ResultManager> {
	public GameObject[] scoreDigits = new GameObject[4];
	public GameObject rankObject;

	public GameObject canvasUiArea;
	public GameObject canvasResultArea;

	// Use this for initialization
	void Start () {
	}

	public void OnCloseClicked () {
		Application.LoadLevel ("StartGame");
	}

	public void ShowResult (int score) {
		canvasUiArea.SetActive (false);
		canvasResultArea.SetActive (true);
		UpdateRank (score);
		UpdateScore (score);
	}

	public void HideResult () {
		Debug.Log ("Hide");
		canvasUiArea.SetActive (true);
		canvasResultArea.SetActive (false);
	}

	void UpdateRank (int score) {
		string imagePath = "";

		if (score < 100) {
			imagePath = "Sprites/result_rank01";
		} else if (score < 500) {
			imagePath = "Sprites/result_rank02";
		} else if (score < 1000) {
			imagePath = "Sprites/result_rank03";
		} else if (score < 5000) {
			imagePath = "Sprites/result_rank04";
		} else {
			imagePath = "Sprites/result_rank05";
		}

		rankObject.GetComponent<Image> ().sprite = Resources.Load(imagePath) as Sprite;
	}
	
	void UpdateScore (int score) {
		score = (score > 9999) ? 9999 : score;
		int[] digits = new int[]{0,0,0,0};
		int index = 0;

		while (score > 0) {
			digits[index] = score % 10;
			score /= 10;
			index ++;
		}

		for (int i = 0; i < 4; i++) {
			if (i >= index) {
				HideScore (scoreDigits[i]);
			}
			else {
				ShowScore (scoreDigits[i], digits[i]);
			}
		}
	}

	void ShowScore (GameObject digitObject, int value) {
		string imagePath = "";

		switch (value) {
		case 0:
			imagePath = "Sprites/0";
			break;
		case 1:
			imagePath = "Sprites/1";
			break;
		case 2:
			imagePath = "Sprites/2";
			break;
		case 3:
			imagePath = "Sprites/3";
			break;
		case 4:
			imagePath = "Sprites/4";
			break;
		case 5:
			imagePath = "Sprites/5";
			break;
		case 6:
			imagePath = "Sprites/6";
			break;
		case 7:
			imagePath = "Sprites/7";
			break;
		case 8:
			imagePath = "Sprites/8";
			break;
		case 9:
			imagePath = "Sprites/9";
			break;
		}

		digitObject.GetComponent<Image> ().sprite = Resources.Load (imagePath) as Sprite;
	}

	void HideScore (GameObject digitObject) {
		digitObject.SetActive (false);
	}
}
