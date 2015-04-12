using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class YakiController : SingletonMonoBehaviourFast<YakiController> {
	private HashSet<GameObject> kushiSet;

	void Start() {
		kushiSet = new HashSet<GameObject> ();
	}

	public void OnClickYakiButton () {
		GameObject matchObject = null;

		foreach (GameObject kushi in kushiSet) {
			if (kushi == null || kushi.gameObject == null) { continue; }
			matchObject = kushi;
			KushiController controller = kushi.GetComponent<KushiController>();
			controller.CompleteYaki ();
			break;
		}

		if (matchObject != null) {
			RemoveKushi (matchObject);
		}
	}

	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Negi") {
			Destroy(collision.gameObject);
		}

		if (collision.gameObject.tag == "Kushi") {
			KushiController controller = collision.gameObject.GetComponent<KushiController>();
			controller.StartYaki ();
		}
	}

	public void RemoveKushi (GameObject removeObject) {
		lock (kushiSet) {
			kushiSet.Remove (removeObject);
		}
	}

	public void AddKushi (GameObject addObject) {
		lock (kushiSet) {
			kushiSet.Add (addObject);
		}
	}
}
