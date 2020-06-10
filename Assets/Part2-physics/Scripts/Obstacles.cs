using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstacles : MonoBehaviour
{
    private float moveSpeed = 3f;
    private bool moveTop = true;
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(body.position.x > 4f)
        {
            moveTop = false;
        }

        if(body.position.x < -4f)
        {
            moveTop = true;
        }

        if(moveTop)
        {
            body.position = new Vector2(body.position.x + moveSpeed * Time.deltaTime, body.position.y);
        }
        else
        {
            body.position = new Vector2(body.position.x - moveSpeed * Time.deltaTime, body.position.y);
        }
    }
}
