using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [Header("Movement Settings")]
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 5.0f;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material blockMaterial;
    [SerializeField] private Material lockMaterial;
    private GameObject objectParent;
    [Header("Waypoint Settings")]
    [SerializeField] private GameObject waypointPrefab;
    private WaypointsList waypoint;
    private Transform currentWaypoint;
    private Quaternion rotationGoal;
    private Vector3 directionToWaypoint;
    private Vector3 waypointFlatPosition;
    private Vector3 carFlatPosition;
    private bool buttonPressed = false;
    // Start is called before the first frame update
    private void Start()
    {
        objectParent = GameObject.FindGameObjectWithTag("WayParent");
        waypoint = FindAnyObjectByType<WaypointsList>();
        AddWaypoint(transform.position);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                Debug.Log("Clicked on: " + clickedObject.name);
                if(clickedObject.tag == "LockNode") {
                    Debug.Log("Locked Node");
                }

                if(clickedObject.tag == "BlockNode") {
                    Debug.Log("Blocked Node");
                }

                if(clickedObject.tag == "WalkNode") {
                    Debug.Log("Valid Node");
                    Vector3 nodeXY = new(clickedObject.transform.position.x, 1.06f, clickedObject.transform.position.z);
                    AddWaypoint(nodeXY);
                }
            }
        }

        if(buttonPressed) {
            MoveTowardsWaypoint();
        }
    }

    private void AddWaypoint(Vector3 position) {
        GameObject newWaypoint = Instantiate(waypointPrefab, Vector3.zero, Quaternion.identity);
                
        newWaypoint.transform.SetParent(objectParent.transform);

        newWaypoint.transform.position = position;
    }

    private void SetNextWaypoint()
    {
        currentWaypoint = waypoint.GetNextWaypoint(currentWaypoint);
    }

    private void MoveTowardsWaypoint()
    {
        waypointFlatPosition = new Vector3(currentWaypoint.position.x, 0, currentWaypoint.position.z);
        carFlatPosition = new Vector3(transform.position.x, 0, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, waypointFlatPosition, moveSpeed * Time.deltaTime);

        if (carFlatPosition == waypointFlatPosition)
        {
            SetNextWaypoint();
        }

        RotateTowardsWaypoint();
    }

    private void RotateTowardsWaypoint()
    {
        directionToWaypoint = (waypointFlatPosition - carFlatPosition).normalized;
        rotationGoal = Quaternion.LookRotation(directionToWaypoint);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, rotationSpeed * Time.deltaTime);
    }

    public void StartButton() {
        SetNextWaypoint();
        buttonPressed = true;
    }
}
