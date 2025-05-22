using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float destroyThreshold = -30f;

    private void Update()
    {
        MoveObstacle();
        CheckForDestruction();
    }

    private void MoveObstacle()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void CheckForDestruction()
    {
        if (transform.position.x < destroyThreshold)
        {
            Destroy(gameObject);
        }
    }
}
