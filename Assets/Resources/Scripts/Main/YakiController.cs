using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class YakiController : SingletonMonoBehaviourFast<YakiController> {
	private SortedList<int, KushiController> kushiSet;
	private int count;

	void Start() {
		count = 0;
		kushiSet = new SortedList<int, KushiController> (new SpitComparer());
	}

	public void OnClickYakiButton () {
		PickUp ();
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

	private void PickUp() {
		KushiController matchObject = null;

		foreach (KushiController kushi in kushiSet.Values) {
			if (kushi == null || kushi.gameObject == null) { continue; }
			matchObject = kushi;
			matchObject.CompleteYaki ();
			break;
		}
		
		if (matchObject != null) {
			RemoveKushi (matchObject);
		}
	}

	public void RemoveKushi (KushiController removeObject) {
		lock (kushiSet) {
			kushiSet.RemoveAt (kushiSet.IndexOfValue(removeObject));
		}
	}

	public void AddKushi (KushiController addObject) {
		lock (kushiSet) {
			int position = kushiSet.IndexOfValue (addObject);
			if (position != -1) {
				kushiSet.RemoveAt (position);
			}

			kushiSet.Add (count, addObject);
			count++;
		}
	}
}

public class SpitComparer : IComparer<int> {
	public int Compare(int x, int y) {
		return x - y;
	}
}
