using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy
{
    private Rigidbody2D rb;
    private Collider2D Coll;
    public Transform Top, Bottom;
    public float Speed;
    private bool isUp = true;
    private float TopY, BottomY;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        Coll = GetComponent<Collider2D>();
        TopY = Top.position.y;
        BottomY = Bottom.position.y;
        Destroy(Top.gameObject);
        Destroy(Bottom.gameObject);

    }


    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, Speed);
            if (transform.position.y > TopY)
            {
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -Speed);
            if (transform.position.y < BottomY)
            {
                isUp = true;
            }
        }
    }

   
}

