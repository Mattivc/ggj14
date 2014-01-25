using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class EtherObjectManager {

	private static List<EtherObject>[] activeEtherObjects = new List<EtherObject>[]{ new List<EtherObject>(), new List<EtherObject>(), new List<EtherObject>() };
	public static List<EtherObject> etherObjects = new List<EtherObject>();

	public static void UpdateFrustum( int colorID, Plane[] frustum, Vector3 viewPoint ) {

		foreach ( EtherObject obj in etherObjects ) {
			if ( GeometryUtility.TestPlanesAABB( frustum, obj.renderer.bounds ) && !IsBlocked( viewPoint, obj.renderer.bounds ) ) {
				if ( !activeEtherObjects[colorID].Contains(obj) ) {
					activeEtherObjects[colorID].Add( obj );
					obj.currentEtherState[colorID] = true;
				}
			} else if ( activeEtherObjects[colorID].Contains( obj ) ) {
				activeEtherObjects[colorID].Remove( obj );
				obj.currentEtherState[colorID] = false;
			}
		}
	}

	private static bool IsBlocked( Vector3 viewPoint, Bounds targetBounds ) {
		Vector3 center = targetBounds.center;
		Vector3 extens = targetBounds.extents;

		Vector3[] points = new Vector3[]{
			new Vector3( center.x, center.y, center.z ),
			new Vector3( center.x + extens.x, center.y + extens.y, center.z + extens.z ),
			new Vector3( center.x + extens.x, center.y + extens.y, center.z - extens.z ),
			new Vector3( center.x + extens.x, center.y - extens.y, center.z + extens.z ),
			new Vector3( center.x + extens.x, center.y - extens.y, center.z - extens.z ),
			new Vector3( center.x - extens.x, center.y + extens.y, center.z + extens.z ),
			new Vector3( center.x - extens.x, center.y + extens.y, center.z - extens.z ),
			new Vector3( center.x - extens.x, center.y - extens.y, center.z + extens.z ),
			new Vector3( center.x - extens.x, center.y - extens.y, center.z - extens.z )
		};

		foreach ( Vector3 point in points ) {
			RaycastHit hit;
			Physics.Raycast( viewPoint, point - viewPoint, out hit );

			if( hit.collider != null && hit.collider.gameObject.layer != LayerMask.NameToLayer("Occluder") ) {
				return false;
			}
		}

		return true;
	}
}
