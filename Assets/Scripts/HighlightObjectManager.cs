using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighlightObjectManager : MonoBehaviour
{

    public static List<HighlightObject> highlightObjects = new List<HighlightObject>();
    static List<HighlightObject> activeHighlightObjects = new List<HighlightObject>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public static void UpdateFrustum(Plane[] frustum)
    {
        foreach (HighlightObject obj in highlightObjects)
        {
            if (GeometryUtility.TestPlanesAABB(frustum, obj.renderer.bounds))
            {
                obj.SendMessage("colorUpdate", frustum);
                if (!activeHighlightObjects.Contains(obj))
                {
                    activeHighlightObjects.Add(obj);
                }
            }
            else if (activeHighlightObjects.Contains(obj))
            {
                activeHighlightObjects.Remove(obj);
                obj.SendMessage("clearColor");
            }
        }
    }
}
