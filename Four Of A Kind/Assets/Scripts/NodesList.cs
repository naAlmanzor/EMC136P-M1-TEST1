using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class NodesList : MonoBehaviour
{
    [SerializeField] public GameObject[] nodeList;

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
[CustomEditor(typeof(NodesList))]
public class NodesListEditor : Editor
{
    int i;

    public override void OnInspectorGUI()
    {
        var thisTarget = (NodesList)target;
        if(thisTarget == null) return;

        DrawDefaultInspector();

        // Adds all node children to the list
        if(GUILayout.Button("Add All Nodes")){
            thisTarget.nodeList = new GameObject[thisTarget.transform.childCount];

            foreach(Transform child in thisTarget.transform)
            {
              thisTarget.nodeList[i++] = child.gameObject;
            }
            
        }

        if(GUILayout.Button("Rename All Nodes")){
            foreach(Transform child in thisTarget.transform)
            {
              child.name = "Node " + (i++).ToString("000");
            }
            
        }
    }
}
#endif