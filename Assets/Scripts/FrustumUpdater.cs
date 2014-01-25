using UnityEngine;
using System.Collections;

public class FrustumUpdater : MonoBehaviour {

	public float etherDetectView = 25f;
	public static Camera playerCamera;

	void Awake() {
		playerCamera = gameObject.camera;
	}

	void LateUpdate() {
		// Player ether object interaction
		
		float oldView = camera.fieldOfView;
		camera.fieldOfView = etherDetectView; // HACK
		EtherObjectManager.UpdateFrustum( 0, GeometryUtility.CalculateFrustumPlanes( camera ) );
        HighlightObjectManager.UpdateFrustum(GeometryUtility.CalculateFrustumPlanes(camera));
		camera.fieldOfView = oldView;
	}
}
