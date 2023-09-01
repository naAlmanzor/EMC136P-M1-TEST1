using UnityEngine;

public class NodeScript : MonoBehaviour
{
    [SerializeField] private bool IsBlocking;
    [SerializeField] private bool IsLocked;
    [SerializeField] private bool IsGoal;
    [SerializeField] private bool waypointNode;
    
    [Header("Node Materials")]
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material blockMaterial;
    [SerializeField] private Material lockMaterial;
    [SerializeField] private Material goalMaterial;

    [SerializeField] private GameStats gameStats;

    void Awake()
    {
        if(IsBlocking){
            gameObject.GetComponent<Renderer>().material = blockMaterial;    
            gameObject.tag = "BlockNode";
        }

        if(IsLocked){
            gameObject.GetComponent<Renderer>().material = lockMaterial;    
            gameObject.tag = "LockNode";   
        }

        if(IsGoal) {
            gameObject.tag = "Finish";
        }

        if(IsGoal && IsLocked) {
            gameObject.GetComponent<Renderer>().material = goalMaterial;
            goalMaterial.SetColor("_Color", Color.yellow);
            gameObject.tag = "LockedFinish";
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if(!IsBlocking && !IsLocked && !IsGoal) {
            gameObject.tag = "WalkNode";  
        }

        if(gameStats.IsKeyObtained() && (gameObject.tag == "LockNode" || gameObject.tag == "LockedFinish")) {
            // gameObject.GetComponent<Renderer>().material = defaultMaterial;
            goalMaterial.SetColor("_Color", Color.green); 
            gameObject.tag = "Finish";   
            IsLocked = false;
        }
    }
}
