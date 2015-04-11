using UnityEngine;
using System.Collections;

public class KushiController : MonoBehaviour {
	public GameObject material1;
	public GameObject material2;
	public GameObject material3;

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
}
