using System.Collections.Generic;
using UnityEngine;

public class PathGameObject : MonoBehaviour {

    public bool debugLines = true;
    public int heightLine = 1;
    public List<GameObject> points = new List<GameObject>();
	
	// Update is called once per frame
	void Update () {
		if ((points.Count > 1) && debugLines)
        {
            DrawDebugLines();
        }
	}

    void DrawDebugLines()
    {
        for (int i=0; i < points.Count; i++)
        {
            Vector3 lineEnd = new Vector3();
            lineEnd.x = points[i].transform.position.x;
            lineEnd.y = points[i].transform.position.y + heightLine;
            lineEnd.z = points[i].transform.position.z;
            Debug.DrawLine(points[i].transform.position, lineEnd);
        }
    }
}

// Klasa pochodna to tylko przykład realizacji a nie wymóg.
