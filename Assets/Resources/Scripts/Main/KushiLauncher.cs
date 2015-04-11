using UnityEngine;
using System.Collections;

public class KushiLauncher : MonoBehaviour {
	public GameObject kushiObject;
	private int count = 0;
	private bool canPrick = false;
	private Collider2D prickCollider;

	void Start () {
		count = 1;
	}

	public void OnClickKushiButton () {
		if (prickCollider != null) {
			InstantiateKushi (prickCollider.gameObject);
		}
	}

	void InstantiateKushi (GameObject negi) {
		GameObject kushi = Instantiate (kushiObject, this.gameObject.transform.position, Quaternion.identity) as GameObject;
		KushiController controller = kushi.GetComponent<KushiController>();
		controller.SetCount(count);
		
		kushi.transform.position = negi.transform.position;

		if (count == 3) {
			Rigidbody2D kushiRigidbody = kushi.GetComponent<Rigidbody2D>();
			Rigidbody2D negiRigidbody = negi.GetComponent<Rigidbody2D>();
			kushiRigidbody.velocity = negiRigidbody.velocity;
		}
		else if (count < 3) {
			Destroy (kushi, 0.1f);
		}
		Destroy (negi);

		// update count
		count = (count >= 3) ? 1 : count + 1;
	}

	void OnTriggerEnter2D (Collider2D collider) {
		prickCollider = collider;
	}

	void OnTriggerExit2D (Collider2D collider) {
		prickCollider = null;
	}
}
