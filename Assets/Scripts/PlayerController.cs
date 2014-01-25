using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject redPlayer;
	public GameObject yellowPlayer;
	public GameObject bluePlayer;

	private bool fadeIn = false;
	private bool fadeOut = false;

	private GameObject currentPlayer;
	private GameObject swapPlayer;

	private float alpha;

	void Start(){
		currentPlayer = redPlayer;

		Rect currentRes = new Rect(-Screen.width * 0.5f, -Screen.height * 0.5f, Screen.width, Screen.height);
		guiTexture.pixelInset = currentRes;

		alpha = guiTexture.color.a;
	}

	void Update () {
		if(!fadeOut && !fadeIn){
			if(Input.GetKeyDown(KeyCode.R) && currentPlayer != redPlayer){
				fadeOut = true;
				swapPlayer = redPlayer;
				FadeStart();
			}else if(Input.GetKeyDown(KeyCode.Y) && currentPlayer != yellowPlayer){
				fadeOut = true;
				swapPlayer = yellowPlayer;
				FadeStart();
			}else if(Input.GetKeyDown(KeyCode.B) && currentPlayer != bluePlayer){
				fadeOut = true;
				swapPlayer = bluePlayer;
				FadeStart();
			}
		}

		if(fadeOut){
			if(guiTexture.color.a < 1f){
				alpha = Mathf.Lerp (alpha, 1.1f, Time.deltaTime*4f);
				guiTexture.color = new Color(0f, 0f, 0f, alpha);
			}else{
				guiTexture.color = new Color(0f, 0f, 0f, 1f);
				SwapPlayer(swapPlayer);
				fadeOut = false;
				fadeIn = true;
			}
		}else if(fadeIn){
			if(guiTexture.color.a > 0f){
				alpha = Mathf.Lerp (alpha, -0.1f, Time.deltaTime*4f);
				guiTexture.color = new Color(0f, 0f, 0f, alpha);
			}else{
				guiTexture.color = new Color(0f, 0f, 0f, 0f);
				FadeComplete();
				fadeIn = false;
			}
		}
	}

	private void FadeStart(){
		guiTexture.enabled = true;
		currentPlayer.GetComponent<MouseLook>().enabled = false;
		currentPlayer.GetComponent<CharacterMotor>().enabled = false;
		currentPlayer.GetComponent<FPSInputController>().enabled = false;
		currentPlayer.transform.FindChild("Main Camera").gameObject.GetComponent<MouseLook>().enabled = false;
	}

	private void SwapPlayer(GameObject newPlayer){
		currentPlayer.transform.FindChild("Main Camera").gameObject.SetActive(false);
		currentPlayer = newPlayer;
		currentPlayer.transform.FindChild("Main Camera").gameObject.SetActive(true);
	}

	private void FadeComplete(){
		guiTexture.enabled = false;
		currentPlayer.GetComponent<MouseLook>().enabled = true;
		currentPlayer.GetComponent<CharacterMotor>().enabled = true;
		currentPlayer.GetComponent<FPSInputController>().enabled = true;
		currentPlayer.transform.FindChild("Main Camera").gameObject.GetComponent<MouseLook>().enabled = true;
	}
}
