using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class NodesList : MonoBehaviour
{
    [SerializeField] public GameObject[] nodeList;
}

#if UNITY_EDITOR
// Custom Editor for the NodesList script
[CustomEditor(typeof(NodesList))]
public class NodesListEditor : Editor
{
    int i; // Counter variable for operations involving arrays

    // Draw the custom inspector
    public override void OnInspectorGUI()
    {
        var thisTarget = (NodesList)target;
        if (thisTarget == null) return;

        // Draw default inspector controls
        DrawDefaultInspector();

        // Adds all node children to the list when the button is clicked
        if (GUILayout.Button("Add All Nodes"))
        {
            // Initialize the nodeList based on the number of children
            thisTarget.nodeList = new GameObject[thisTarget.transform.childCount];
            i = 0;

            // Populate the nodeList
            foreach (Transform child in thisTarget.transform)
            {
                thisTarget.nodeList[i++] = child.gameObject;
            }
        }

        // Renames all nodes when the button is clicked
        if (GUILayout.Button("Rename All Nodes"))
        {
            i = 0;
            foreach (Transform child in thisTarget.transform)
            {
                child.name = "Node " + (i++).ToString("000");
            }
        }
    }
}
#endif
