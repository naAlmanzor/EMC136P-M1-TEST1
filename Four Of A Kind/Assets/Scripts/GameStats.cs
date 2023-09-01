using UnityEngine;

[CreateAssetMenu(fileName = "GameStats", menuName = "Four Of A Kind/GameStats", order = 0)]
public class GameStats : ScriptableObject {
    [SerializeField] private bool keyObtained = false;
    [SerializeField] private int waypointCount = 4;
    public string previousLevel;
    public bool levelCleared = false;

    public int GetWaypointCount()
    {
        return waypointCount;
    }

    public bool IsKeyObtained()
    {
        return keyObtained;
    }

    public void SetBool(bool boolean)
    {
        keyObtained = boolean;
    }

    public void SetCount(int num)
    {
        waypointCount += num;
    }

    public void Reset()
    {
        waypointCount = 4;
        keyObtained = false;
        levelCleared = false;
    }
}