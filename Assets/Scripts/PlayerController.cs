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
	private Vector3[] playerPositions;
	private Quaternion[] playerRotations;

	void Start(){
		currentPlayer = redPlayer;

		Rect currentRes = new Rect(-Screen.width * 0.5f, -Screen.height * 0.5f, Screen.width, Screen.height);
		guiTexture.pixelInset = currentRes;

		alpha = guiTexture.color.a;

		playerPositions = new Vector3[]{redPlayer.transform.position, yellowPlayer.transform.position, bluePlayer.transform.position};
		playerRotations = new Quaternion[]{redPlayer.transform.rotation, yellowPlayer.transform.rotation, bluePlayer.transform.rotation};
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

	public void UpdateCheckpoint(Vector3 checkpointPosition, Quaternion checkpointRotation){
		playerPositions[0] = checkpointPosition;
		playerPositions[1] = new Vector3(checkpointPosition.x + 2f, checkpointPosition.y, checkpointPosition.z);
		playerPositions[2] = new Vector3(checkpointPosition.x - 2f, checkpointPosition.y, checkpointPosition.z);

		playerRotations[0] = checkpointRotation;
		playerRotations[1] = checkpointRotation;
		playerRotations[2] = checkpointRotation;
	}

	public void RestartCheckpoint(){
		fadeOut = true;
		swapPlayer = null;
		FadeStart();
	}

	private void FadeStart(){
		guiTexture.enabled = true;
		currentPlayer.GetComponent<MouseLook>().enabled = false;
		currentPlayer.GetComponent<CharacterMotor>().canControl = false;
		currentPlayer.GetComponent<FPSInputController>().enabled = false;
		currentPlayer.transform.FindChild("Holder").gameObject.GetComponent<MouseLook>().enabled = false;
	}

	private void SwapPlayer(GameObject newPlayer){
        currentPlayer.transform.Find("Holder/Main Camera").camera.enabled = false;

            if (newPlayer != null)
            {
                {
                    currentPlayer = newPlayer;

                    if (currentPlayer == redPlayer)
                    {
                        HighlightObject.highlightColor = new Color(1f, 0f, 0f);
                    }
                    else if(currentPlayer == bluePlayer)
                    {
                        HighlightObject.highlightColor = new Color(0f, 0f, 1f);
                    }
                    else if(currentPlayer == redPlayer)
                    {
                        HighlightObject.highlightColor = new Color(1f, 1f, 0f);
                    }  
                    
                }
            }
            else
            {
				redPlayer.transform.position = playerPositions[0];
				redPlayer.transform.rotation = playerRotations[0];
				redPlayer.GetComponent<CharacterController>().Move(Vector3.zero);
				
				yellowPlayer.transform.position = playerPositions[1];
				yellowPlayer.transform.rotation = playerRotations[1];
				yellowPlayer.GetComponent<CharacterController>().Move(Vector3.zero);
					
				bluePlayer.transform.position = playerPositions[2];
				bluePlayer.transform.rotation = playerRotations[2];
				bluePlayer.GetComponent<CharacterController>().Move(Vector3.zero);

				currentPlayer = redPlayer;
			}

			currentPlayer.transform.Find("Holder/Main Camera").camera.enabled = true;
	}

	private void FadeComplete(){
		guiTexture.enabled = false;
		currentPlayer.GetComponent<MouseLook>().enabled = true;
		currentPlayer.GetComponent<CharacterMotor>().canControl = true;
		currentPlayer.GetComponent<FPSInputController>().enabled = true;
		currentPlayer.transform.FindChild("Holder").gameObject.GetComponent<MouseLook>().enabled = true;
	}
}
