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
	}

	void Start() {
		renderer.material.color = objectColor;
	}

	void OnEnable() {
		EtherObjectManager.etherObjects.Add(this);
	}

	void OnDestroy() {
		EtherObjectManager.etherObjects.Remove(this);
	}

	void Update() {
		bool currentState = currentEtherState[0] == redState && currentEtherState[1] == yellowState && currentEtherState[2] == blueState;

		if ( currentState != objectActive ) {
			gameObject.SendMessage("ToggleEtherObject", currentState, SendMessageOptions.DontRequireReceiver );
			objectActive = currentState;
		}
	}

	void LateUpdate() {
		float alpha = objectActive ? 1.0f : 0.3f;

		renderer.material.color = Color.Lerp( renderer.material.color, new Color( objectColor.r, objectColor.g, objectColor.b, alpha), 5f * Time.deltaTime );
	}

}
