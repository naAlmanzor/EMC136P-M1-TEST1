using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    [Header("Movement Settings")]
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 5.0f;
    private GameObject objectParent;
    [Header("Waypoint Settings")]
    [SerializeField] private GameObject waypointPrefab;
    private WaypointsList waypoint;
    private Transform currentWaypoint;
    private Quaternion rotationGoal;
    private Vector3 directionToWaypoint;
    private Vector3 waypointFlatPosition;
    private Vector3 playerFlatPosition;
    private bool buttonPressed = false;
    private int maxWP = 4;
    private bool reachedLastWaypoint = false;
    [SerializeField] private TextMeshProUGUI wpText;
    [SerializeField] private Button moveButton, undoButton;

    [Header("GameStatsSO")]
    [SerializeField] private GameStats gameStats;

    private void Start()
    {
        gameStats.Reset();
        objectParent = GameObject.FindGameObjectWithTag("WayParent");
        waypoint = FindAnyObjectByType<WaypointsList>();

        Vector3 playerXY = new(transform.position.x, 1.06f, transform.position.z);
        AddWaypoint(playerXY);

        moveButton = GameObject.FindGameObjectWithTag("StartButton").GetComponent<Button>();
        undoButton = GameObject.FindGameObjectWithTag("UndoButton").GetComponent<Button>();
    }

    private void Update()
    {
        UpdateWaypointText();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                
                if (clickedObject.tag == "WalkNode" || clickedObject.tag == "Finish" || clickedObject.tag == "LockedFinish" || clickedObject.tag == "Key")
                {
                    // Debug.Log("Valid Node");
                    if (gameStats.GetWaypointCount() != 0)
                    {
                        gameStats.SetCount(-1);
                        Vector3 nodeXY = new(clickedObject.transform.position.x, 1.06f, clickedObject.transform.position.z);
                        AddWaypoint(nodeXY);
                        Debug.Log("Waypoint Added");
                    }
                }
            }
        }

        if(buttonPressed) 
        {

            moveButton.enabled = false;
            moveButton.image.color = new(
                moveButton.image.color.r,
                moveButton.image.color.g,
                moveButton.image.color.b,
                0.3f
            );
            undoButton.enabled = false;
            undoButton.image.color = new(
                undoButton.image.color.r,
                undoButton.image.color.g,
                undoButton.image.color.b,
                0.3f
            );
            MoveTowardsWaypoint();
        }
        else
        {
            moveButton.enabled = true;
            moveButton.image.color = new(
                moveButton.image.color.r,
                moveButton.image.color.g,
                moveButton.image.color.b,
                1f
            );
            undoButton.enabled = true;
            undoButton.image.color = new(
                undoButton.image.color.r,
                undoButton.image.color.g,
                undoButton.image.color.b,
                1f
            );
        }

        if(reachedLastWaypoint && !gameStats.levelCleared)
        {
            GameOver();
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
        if(currentWaypoint != null)
        {
            waypointFlatPosition = new Vector3(currentWaypoint.position.x, 0, currentWaypoint.position.z);
            playerFlatPosition = new Vector3(transform.position.x, 0, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, waypointFlatPosition, moveSpeed * Time.deltaTime);

            if (playerFlatPosition == waypointFlatPosition)
            {
                Transform nextWaypoint = waypoint.GetNextWaypoint(currentWaypoint);
                if (nextWaypoint == null)
                {
                    reachedLastWaypoint = true;
                    buttonPressed = false;
                    // Debug.Log("Reached the last waypoint");
                }
                else
                {
                    SetNextWaypoint();
                }
            }

            RotateTowardsWaypoint();
        }
    }

    private void RotateTowardsWaypoint()
    {
        directionToWaypoint = (waypointFlatPosition - playerFlatPosition).normalized;
        rotationGoal = Quaternion.LookRotation(directionToWaypoint);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, rotationSpeed * Time.deltaTime);
    }

    public void StartButton()
    {
        SetNextWaypoint();
        buttonPressed = true;
    }

    private void UpdateWaypointText()
    {
        wpText.text = gameStats.GetWaypointCount() + "/" + maxWP;
    }

    public void Undo()
    {
        if(gameStats.GetWaypointCount() !=  maxWP) {
            gameStats.SetCount(+1);
        }
        // else Debug.Log("Can't Undo");
    }

    private void OnTriggerStay(Collider other) {
        if(other.tag == "WalkNode")
        {
            // Debug.Log("Walk");
        }

        if(other.tag == "BlockNode" || other.tag == "LockNode")
        {
            // gameStats.levelCleared = false;
            GameOver();
        }

        if(other.tag == "Finish")
        {
            gameStats.levelCleared = true;
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Key") {
            // Debug.Log("Key");
            other.gameObject.SetActive(false);
            gameStats.SetBool(true);
        }
    }

    private void GameOver()
    {
        gameStats.previousLevel = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("GameStateScene");
    }
}
