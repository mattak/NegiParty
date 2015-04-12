using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UniRx;

public class GameManager : SingletonMonoBehaviourFast<GameManager> {
	public Text scoreText;
	public float score = 0.0f;
	public Image gameTimeImage;
	public float maxGameTime = 30.0f;
	private float gameStartTime;
	private Vector2 maxGaugeSize;

	// Use this for initialization
	void Start () {
		SetScore (0.0f);

		gameStartTime = Time.time;
		maxGaugeSize = gameTimeImage.rectTransform.sizeDelta;

		Observable.Timer(System.TimeSpan.FromSeconds(0.1f))
			.RepeatUntilDestroy(this.gameObject)
				.Select (_ => {
					float timeDifference = Time.time - gameStartTime;
					float ratio = timeDifference / maxGameTime;
					return (ratio > 1.0f) ? 1.0f : ratio;
				})
				.Subscribe (ratio => {
					Vector2 size = new Vector2(maxGaugeSize.x * ratio, maxGaugeSize.y);
					gameTimeImage.rectTransform.sizeDelta = size;

					if (ratio >= 1.0f) {
						GameOver();
					}
				});

		ResultManager.Instance.HideResult ();
	}

	public void SetScore (float point) {
		score = 0.0f;
		scoreText.text = "" + score;
	}

	public void UpdateScore (float point) {
		score += point;
		scoreText.text = "" + score;
	}

	public void GameOver () {
		PlayerPrefs.SetFloat ("lastScore", score);
		ResultManager.Instance.ShowResult ((int)score);
	}
}
