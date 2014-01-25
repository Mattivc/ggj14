using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {

	private PlayerController playerHandler;
	
	void Start(){
		playerHandler = GameObject.Find("_PlayerHandler").GetComponent<PlayerController>();
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag=="Player"){
			playerHandler.RestartCheckpoint();
		}
	}
}
