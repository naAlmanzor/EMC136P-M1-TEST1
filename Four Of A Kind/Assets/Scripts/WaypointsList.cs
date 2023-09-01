using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class WaypointsList : MonoBehaviour
{
    [Range (0f, 2f)]
    [SerializeField] private float waypointSize = 1f;
    [SerializeField] public GameObject[] waypointList;
    private LineRenderer lineRenderer;

    private void OnDrawGizmos(){
        
        if(transform.childCount != 0) {
            foreach(Transform t in transform)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(t.position, waypointSize);
            }
            
            Gizmos.color = Color.red;

            for(int i = 0; i < transform.childCount - 1; i++){
                Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i+1).position);
            }

            // Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
            // Debug.Log(transform.GetChild(0).name);
        }
    }

    public Transform GetNextWaypoint(Transform waypoint)
    { 
        if(waypoint == null)
        {
            return transform.GetChild(0);
        }

        int nextIndex = waypoint.GetSiblingIndex() + 1;

        if(nextIndex >= transform.childCount)
        {
            return null;
        }

        return transform.GetChild(nextIndex);
    }

    public void RemoveLastWaypoint()
    {
        if(transform.childCount > 1)
        {
            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
        }
        else Debug.Log("Can't destroy starting position");
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        if(lineRenderer == null) {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.material = new Material(Shader.Find("Standard"))
        {
            color = Color.green
        };
        lineRenderer.widthMultiplier = 0.5f; // Set the width of the line
    }


    // Update is called once per frame
    private void Update() 
    {
        UpdateLineRenderer();
    }

    private void UpdateLineRenderer() 
    {
        Vector3[] points = new Vector3[transform.childCount];
        
        for (int i = 0; i < transform.childCount; i++) {
            points[i] = transform.GetChild(i).position;
        }
        
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(WaypointsList))]
public class WaypointsListEditor : Editor
{
    int i;

    public override void OnInspectorGUI()
    {
        var thisTarget = (WaypointsList)target;
        if(thisTarget == null) return;

        DrawDefaultInspector();

        // Adds all node children to the list
        if(GUILayout.Button("Add All Nodes")){
            thisTarget.waypointList = new GameObject[thisTarget.transform.childCount];

            foreach(Transform child in thisTarget.transform)
            {
              thisTarget.waypointList[i++] = child.gameObject;
            }
            
        }
    }
}
#endif
