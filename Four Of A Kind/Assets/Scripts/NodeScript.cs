using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    [SerializeField] private bool IsBlocking;
    [SerializeField] private bool IsLocked;
    [SerializeField] private Material blockMaterial;
    [SerializeField] private Material lockMaterial;

    void Awake()
    {
        if(IsBlocking == true){
            this.gameObject.GetComponent<Renderer>().material = blockMaterial;    
        }

        if(IsBlocking == true && IsLocked == true){
            this.gameObject.GetComponent<Renderer>().material = lockMaterial;    
        }
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
