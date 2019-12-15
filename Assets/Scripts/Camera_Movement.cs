using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        Movement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Movement()
    {
        rigidbody2d.velocity = new Vector2(+speed, rigidbody2d.velocity.y);
    }
}
