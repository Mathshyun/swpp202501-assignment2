using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Vector3[] waypoints;
    public float duration;
    
    private int _currentWaypointIndex;
    private float _elapsedTime;
    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private void Start()
    {
        _currentWaypointIndex = 0;
        _elapsedTime = 0f;
        transform.position = waypoints[_currentWaypointIndex];
        SetNextWaypoint();
    }

    private void Update()
    {
        if (_elapsedTime >= duration)
        {
            SetNextWaypoint();
        }
        else
        {
            transform.position = Vector3.Lerp(_startPosition, _endPosition, _elapsedTime / duration);
        }
        
        _elapsedTime += Time.deltaTime;
    }

    private void SetNextWaypoint()
    {
        _startPosition = waypoints[_currentWaypointIndex];
        _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
        _endPosition = waypoints[_currentWaypointIndex];
        _elapsedTime = 0f;
    }
}
