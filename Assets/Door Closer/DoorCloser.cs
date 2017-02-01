using UnityEngine;
using System.Collections;

public class DoorCloser : MonoBehaviour {

	public GameObject door;
	bool closed;

	void OnTriggerEnter2D(Collider2D collider) {

		if (closed) return;

		if (collider.tag == "Gun") {
			door.SetActive(true);
			closed = true;
		}
	}
}
