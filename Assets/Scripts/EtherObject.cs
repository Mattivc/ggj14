using UnityEngine;
using System.Collections;

public class EtherObject : MonoBehaviour {

	void Start(){
		gameObject.GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0f, 0.2f);
	}

	void Update () {
	
	}

	private void OnEtherActivate(){

	}

	private void OnEtherDeactivate(){

	}
}
