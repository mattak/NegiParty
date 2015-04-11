using UnityEngine;
using System.Collections;
using UniRx;

public class NegiLauncher : MonoBehaviour { 
	public GameObject material;
	public float velocityStrength = 10;

	// Use this for initialization
	void Start () {

		long count = 0;
		Observable.Timer(System.TimeSpan.FromSeconds(0.5))
			.RepeatUntilDestroy(this.gameObject)
				.Where( x => {
					return (++count) % 4 != 0;
				})
				.Where( x => {
					return (count / 4) % 4 != 0;
				})
			.Subscribe(x => LaunchMaterial());
	}

	void LaunchMaterial () {
		GameObject gameObject = Instantiate (material, this.gameObject.transform.position, Quaternion.identity) as GameObject;
		Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();

		float velocityDiverseX = Random.Range (-0.1f ,0.1f);
		float velocityDiverseY = Random.Range (0.0f, 0.1f);
		rigidbody.velocity = new Vector2 ((1.2f + velocityDiverseX)*velocityStrength, (1.8f + velocityDiverseY)*velocityStrength);

		Destroy (gameObject, 3.0f);
	}
}
