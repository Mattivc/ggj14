using UnityEngine;
using System.Collections;

public class HighlightObject : MonoBehaviour {

    Mesh mesh;
    Vector3[] vertices;
    Color[] colors;
    Bounds[] vertBounds;

	void Start () {

        mesh = gameObject.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        colors = new Color[vertices.Length];
        vertBounds = new Bounds[vertices.Length];
	}

    void OnEnable()
    {
        HighlightObjectManager.highlightObjects.Add(this);
    }

    void OnDestroy()
    {
        HighlightObjectManager.highlightObjects.Remove(this);
    }

    void colorUpdate(Plane[] frustum)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            vertBounds[i] = new Bounds(transform.TransformPoint(vertices[i]), new Vector3(0f, 0f, 0f));

            if (GeometryUtility.TestPlanesAABB(frustum, vertBounds[i]))
            {
                colors[i] = new Color(1f, 0f, 0f);
            }
            else
                colors[i] = new Color(1f, 1f, 1f);
        }
        mesh.colors = colors;
    }

    void clearColor()
    {
        for (int i = 0; i < colors.Length; i++)
        {
                colors[i] = new Color(1f, 1f, 1f);
        }
        mesh.colors = colors;
    }
}
