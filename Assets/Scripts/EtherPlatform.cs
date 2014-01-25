using UnityEngine;
using System.Collections;

public class EtherPlatform : MonoBehaviour {

	void Start(){
		ToggleEtherObject(false);
	}

	void ToggleEtherObject (bool state) {
		gameObject.collider.enabled = state;
	}
}
