using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    [SerializeField] private float moveSpeed = 5.0f;
    private int currentPoint = 0;

    private void Update()
    {
        Vector3 point = points[currentPoint].position;
        if (transform.position != point)
            transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);
        else
            currentPoint = currentPoint < points.Count - 1 ? currentPoint + 1 : 0;
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
    */
}
