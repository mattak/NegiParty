using UnityEngine;
using System.Collections;

public class YakiController : MonoBehaviour {
	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Negi") {
			Destroy(collision.gameObject);
		}
		if (collision.gameObject.tag == "Kushi") {
			KushiController controller = collision.gameObject.GetComponent<KushiController>();
			controller.StartYaki ();
		}
	}
}
