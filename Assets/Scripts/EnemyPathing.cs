using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyPathing : MonoBehaviour
{
    // Config params
    [SerializeField] private WaveConfig WaveConfig;
    [SerializeField] private float moveSpeed = 2f;

    private int waypointIndex = 0;
    private List<Transform> waypoints;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = WaveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex < waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex+1].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards
                (transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
