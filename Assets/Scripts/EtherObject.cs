using UnityEngine;
using System.Collections;

public class EtherObject : MonoBehaviour {

	public static readonly ColorRYB[] stateColors = new ColorRYB[]{ ColorRYB.Red, ColorRYB.Yellow, ColorRYB.Blue };

	public Color objectColor = Color.black;
	private bool objectActive = false;

	public bool redState = false;
	public bool yellowState = false;
	public bool blueState = false;

	public bool[] currentEtherState;

	void Awake() {
		currentEtherState = new bool[]{ false, false, false };

		SetEtherLayer();
	}

	void  Start() {
		renderer.material.color = objectColor;
	}

	void OnEnable() {
		EtherObjectManager.etherObjects.Add(this);
	}

	void OnDestroy() {
		EtherObjectManager.etherObjects.Remove(this);
	}

	void Update() {
		//bool currentState = currentEtherState[0] == redState && currentEtherState[1] == yellowState && currentEtherState[2] == blueState;
		bool currentState = true;

		if(redState && !currentEtherState[0]){
			currentState = false;
		}
		if(yellowState && !currentEtherState[1]){
			currentState = false;
		}
		if(blueState && !currentEtherState[2]){
			currentState = false;
		}


		if ( currentState != objectActive ) {
			gameObject.SendMessage("ToggleEtherObject", currentState, SendMessageOptions.DontRequireReceiver );
			objectActive = currentState;
			if(!currentState){
				SetEtherLayer();
			}else{
				gameObject.layer = LayerMask.NameToLayer("Default");
			}
		}
	}

	void LateUpdate() {
		float alpha = objectActive ? 1.0f : 0.3f;

		renderer.material.color = Color.Lerp( renderer.material.color, new Color( objectColor.r, objectColor.g, objectColor.b, alpha), 5f * Time.deltaTime );
	}

	private void SetEtherLayer(){
		if ( redState && !yellowState && !blueState )  { // RED
			gameObject.layer = LayerMask.NameToLayer("Red");
		} else if ( !redState && yellowState && !blueState ) { // YELLOW
			gameObject.layer = LayerMask.NameToLayer("Yellow");
		} else if ( !redState && !yellowState && blueState ) { // BLUE
			gameObject.layer = LayerMask.NameToLayer("Blue");
		} else if ( redState && yellowState && !blueState ) { // ORANGE
			gameObject.layer = LayerMask.NameToLayer("Orange");
		} else if ( !redState && yellowState && blueState ) { // GREEN
			gameObject.layer = LayerMask.NameToLayer("Green");
		} else if ( redState && !yellowState && blueState ) { // PURPLE
			gameObject.layer = LayerMask.NameToLayer("Purple");
		} else {
			gameObject.layer = LayerMask.NameToLayer("Default");
		}
	}
}
