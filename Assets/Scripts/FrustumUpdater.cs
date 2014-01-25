using UnityEngine;
using System.Collections;

public class FrustumUpdater : MonoBehaviour {

	public float etherDetectView = 25f;
	public static Camera playerCamera;
	public int color = 0;

	void Awake() {
		playerCamera = gameObject.camera;
	}

	void LateUpdate() {
		// Player ether object interaction
		
		float oldView = camera.fieldOfView;
		camera.fieldOfView = etherDetectView; // HACK
		EtherObjectManager.UpdateFrustum( color, GeometryUtility.CalculateFrustumPlanes( camera ), transform.position );
        HighlightObjectManager.UpdateFrustum(GeometryUtility.CalculateFrustumPlanes(camera));
		camera.fieldOfView = oldView;
	}
}
