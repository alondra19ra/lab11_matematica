using System.Collections;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    [SerializeField] private GameObject highPipePrefab;
    [SerializeField] private GameObject lowPipePrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float minYPosition = 9f;
    [SerializeField] private float maxYPosition = 20f;
    [SerializeField] private float gap = 25f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnPipes), 0f, spawnInterval);
    }

    private void SpawnPipes()
    {
        float position = Random.Range(minYPosition, maxYPosition);
        Vector3 highObstaclePosition = new Vector3(transform.position.x, position, transform.position.z);
        Vector3 lowObstaclePosition = new Vector3(transform.position.x, position - gap, transform.position.z);

        Instantiate(highPipePrefab, highObstaclePosition, Quaternion.identity);
        Instantiate(lowPipePrefab, lowObstaclePosition, Quaternion.identity);
    }
}


