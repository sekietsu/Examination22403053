using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 direction;

    void Start()
    {
        SetRandomDirection();
        InvokeRepeating("SetRandomDirection", 2f, 2f);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if (transform.position.x < -8f && transform.position.x > 8f)
            direction.x *= -1;
        if (transform.position.y < -5f && transform.position.y > 5f)
            direction.y *= -1;
    }

    void SetRandomDirection()
    {
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
    }

}
