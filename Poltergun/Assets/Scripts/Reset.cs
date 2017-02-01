using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Reset : MonoBehaviour {

	public KeyCode resetKey = KeyCode.R;

	void Update() {
		if (Input.GetKeyDown(resetKey)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
