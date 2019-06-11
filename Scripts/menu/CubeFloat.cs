using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFloat : MonoBehaviour
{
    
    private float speed = 0.2f, tilt = 40f;
    private Vector3 target  = new Vector3(0, 0.5f, 0);
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        if (transform.position == target && transform.position.y != -0.3f) target.y = -0.3f;
        if (transform.position == target && transform.position.y == -0.3f) target.y = 0.5f;
        transform.Rotate(Vector3.up * Time.deltaTime * tilt);
        if (transform.position.y < -2f || transform.position.x > 3 || transform.position.x < -3) Destroy(gameObject); 
    }
    void OnMouseUpAsButton() => GetComponent<Rigidbody2D>().gravityScale = 1.5f;
    
}

