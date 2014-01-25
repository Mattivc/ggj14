using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class EtherObjectManager {

	private static List<EtherObject>[] activeEtherObjects = new List<EtherObject>[]{ new List<EtherObject>(), new List<EtherObject>(), new List<EtherObject>() };
	public static List<EtherObject> etherObjects = new List<EtherObject>();

	public static void UpdateFrustum( int colorID, Plane[] frustum ) {

		foreach ( EtherObject obj in etherObjects ) {
			if ( GeometryUtility.TestPlanesAABB( frustum, obj.renderer.bounds ) ) {
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
}
