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
    private void OnDrawGizmos(){
        
        foreach(Transform t in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, waypointSize);
        }
        
        Gizmos.color = Color.red;

        for(int i = 0; i < transform.childCount - 1; i++){
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i+1).position);
        }

        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
        // Debug.Log(transform.GetChild(0).name);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
