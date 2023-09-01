using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    [SerializeField] private bool IsBlocking;
    [SerializeField] private bool IsLocked;
    [SerializeField] private bool IsGoal;
    [SerializeField] private bool waypointNode;
    
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material blockMaterial;
    [SerializeField] private Material lockMaterial;

    [SerializeField] private GameStats gameStats;

    void Awake()
    {
        if(IsBlocking){
            gameObject.GetComponent<Renderer>().material = blockMaterial;    
            gameObject.tag = "BlockNode";
            // this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        if(IsLocked){
            gameObject.GetComponent<Renderer>().material = lockMaterial;    
            gameObject.tag = "LockNode";   
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
        if(IsGoal) {
            gameObject.tag = "Finish";
        }
        if(!IsBlocking && !IsLocked && !IsGoal) {
            gameObject.tag = "WalkNode";  
        }

        if(gameStats.IsKeyObtained() && gameObject.tag == "LockNode") {
            gameObject.GetComponent<Renderer>().material = defaultMaterial;  
            gameObject.tag = "WalkNode";   
            IsLocked = false;
        }
    }
}
