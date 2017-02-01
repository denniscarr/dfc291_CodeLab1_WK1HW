using UnityEngine;
using System.Collections;

public class ParticleAutoDelete : MonoBehaviour {

	ParticleSystem ps;

	void Start() {
		ps = GetComponentInChildren<ParticleSystem>();
	}

	void Update() {
		if (ps.isStopped) {
			Destroy(gameObject);
		}
	}

}
