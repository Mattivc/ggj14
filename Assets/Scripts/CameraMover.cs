using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {

	private float moveSpeed = 3f;
	private Vector2 sensitivity = new Vector2( 1.5f, 1.5f );
	private float yMaxMin = 60f;

	private Vector2 rotVec = Vector2.zero;
	private Quaternion startRot;

	void Start() {
		startRot = transform.rotation;
		Screen.lockCursor = true;
	}

	void Update () {

		// Player movement and rotation

		Vector3 moveVec = Vector3.zero;

		if ( Input.GetKey(KeyCode.W) ) {
			moveVec += Vector3.forward;
		}

		if ( Input.GetKey(KeyCode.A) ) {
			moveVec += Vector3.left;
		}

		if ( Input.GetKey(KeyCode.S) ) {
			moveVec += Vector3.back;
		}

		if ( Input.GetKey(KeyCode.D) ) {
			moveVec += Vector3.right;
		}

		if ( Input.GetKey(KeyCode.Q) ) {
			moveVec += Vector3.up;
		}

		if ( Input.GetKey(KeyCode.E) ) {
			moveVec += Vector3.down;
		}


		rotVec += new Vector2( Input.GetAxis("Mouse X") * sensitivity.x, Input.GetAxis("Mouse Y") * sensitivity.y );
		rotVec = new Vector2( ClampAngle( rotVec.x, -360f, 360f ), ClampAngle( rotVec.y, -yMaxMin, yMaxMin) );

		//Quaternion xQuat = Quaternion.AngleAxis( rotVec.x, Vector3.up );
		//Quaternion yQuat = Quaternion.AngleAxis( rotVec.y, Vector3.left );

		transform.localEulerAngles = new Vector3( rotVec.y, rotVec.x, 0f );
		transform.Translate( moveVec * moveSpeed * Time.deltaTime );


	}

	public static float ClampAngle (float angle, float min, float max) {
		if (angle < -360f) {
			angle += 360f;
		}
		if (angle > 360f) {
			angle -= 360f;
		}
		return Mathf.Clamp (angle, min, max);
	}

}
